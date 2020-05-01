namespace BlazorApp.Shared.Validation
{
    /// <summary>
    /// </summary>
    public class ValidationMessage
    {
        /// <summary>
        ///     Creates validation message
        /// </summary>
        /// <param name="name">Validation name</param>
        /// <param name="message">Validation message</param>
        /// <param name="args">Validation message arguments</param>
        public ValidationMessage(string name, string message, object[] args = null)
        {
            Name = name;
            Message = message;
            Args = args ?? new object[0];
        }

        /// <summary>
        ///     Gets validation name.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        ///     Gets validation message.
        /// </summary>
        public string Message { get; private set; }

        /// <summary>
        ///     Gets validation mesage arguments 
        /// </summary>
        public object[] Args { get; private set; }
    }
}