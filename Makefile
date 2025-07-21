# Copyright 2017 The Nuclio Authors.
#
# Licensed under the Apache License, Version 2.0 (the "License");
# you may not use this file except in compliance with the License.
# You may obtain a copy of the License at
#
#	 http://www.apache.org/licenses/LICENSE-2.0
#
# Unless required by applicable law or agreed to in writing, software
# distributed under the License is distributed on an "AS IS" BASIS,
# WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
# See the License for the specific language governing permissions and
# limitations under the License.

PROJECT_PATH ?= $(shell pwd)


.PHONY: test
test: build-test-image
	docker run --rm nuclio-test

.PHONY: fmt
fmt: build-test-image
	docker run --rm --volume $(PROJECT_PATH):/nuclio nuclio-test /root/.dotnet/tools/dotnet-format --folder

.PHONY: lint
lint: build-test-image
	docker run --rm --volume $(PROJECT_PATH):/nuclio nuclio-test /root/.dotnet/tools/dotnet-format -v diag --check --folder


.PHONY: build-test-image
build-test-image:
	@docker build -f tests/Dockerfile -t nuclio-test .

.PHONY: bump-dependencies
bump-dependencies:
	@echo "🔄 Ensuring dotnet-outdated is installed..."
	dotnet tool restore || dotnet tool install --global dotnet-outdated-tool
	@echo "📦 Bumping root-level dependencies..."
	dotnet outdated --upgrade
	@echo "🧪 Bumping test project dependencies..."
	cd tests && dotnet outdated --upgrade

