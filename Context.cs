namespace nuclio_sdk_dotnetcore
{
    public class Context
    {
        public Logger Logger { get; set; }


        public Context()
        {
            Logger = new Logger();

        }
    }
}