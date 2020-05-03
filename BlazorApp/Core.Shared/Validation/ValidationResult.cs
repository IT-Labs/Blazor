using System.Collections.Generic;
using System.Linq;

namespace Core.Shared.Validation
{
    /// <summary>
    ///     Definition of the ValidationResult
    /// </summary>
    public class ValidationResult
    {
        /// <summary>
        ///     Base constructor. Sets Dirty as true and new ValidationMassage list
        /// </summary>
        public ValidationResult()
        {
            _dirty = true;
            Messages = new List<ValidationMessage>();
        }

        /// <summary>
        ///     Adds a validation error to the result.
        /// </summary>
        /// <param name="errorName">The name of the validation error.</param>
        /// <param name="errorMessage">The error message.</param>
        /// <param name="args">The error message arguments.</param>
        public void AddValidationError(string errorName, string errorMessage, params object[] args)
        {
            _dirty = true;
            Messages.Add(new ValidationMessage(errorName, errorMessage, args));
        }

        private bool _dirty;
        private ValidationStatus _state;

        /// <summary>
        ///     Gets or Sets definition of Validation messages
        /// </summary>
        public IList<ValidationMessage> Messages { get; set; }

        /// <summary>
        ///     Gets whether the validation result is valid.
        /// </summary>
        public ValidationStatus State
        {
            get
            {
                if (_dirty)
                {
                    _dirty = false;
                    _state = Messages.Any() ? ValidationStatus.Invalid : ValidationStatus.Valid;
                }

                return _state;
            }
            set
            {
                _dirty = false;
                _state = value;
            }
        }

        public void SetDirty()
        {
            _dirty = true;
        }
    }
}