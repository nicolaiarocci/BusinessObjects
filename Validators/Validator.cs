using System.Reflection;

namespace BusinessObjects.Validators {
    /// <summary>
    /// An abstract class that contains information about a rule as well as a method to validate it.
    /// </summary>
    /// <remarks>
    /// This class is primarily designed to be used on a domain object to validate a business rule. In most cases, you will want to use the 
    /// concrete class SimpleRule, which just needs you to supply a delegate used for validation. For custom, complex business rules, you can 
    /// extend this class and provide your own method to validate the rule.
    /// </remarks>
    public abstract class Validator {
        internal string _description;
        private string _propertyName;

        /// <summary>
        /// Constructor.
        /// </summary>
        public Validator() { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="propertyName">The name of the property the rule is based on. This may be blank if the rule is not for any specific property.</param>
        /// <param name="description">A description of the rule that will be shown if the rule is broken.</param>
        public Validator(string propertyName, string description) {
            this.Description = description;
            this.PropertyName = propertyName;
        }

        /// <summary>
        /// Gets descriptive text about this broken rule.
        /// </summary>
        public virtual string Description {
            get { return _description; }
            internal set { _description = value; }
        }

        /// <summary>
        /// Gets the name of the property the rule belongs to.
        /// </summary>
        public virtual string PropertyName {
            get { return (_propertyName ?? string.Empty).Trim(); }
            internal set { _propertyName = value; }
        }

        /// <summary>
        /// Validates that the rule has been followed.
        /// </summary>
        public abstract bool Validate(DomainObject domainObject);

        /// <summary>
        /// Gets a string representation of this rule.
        /// </summary>
        /// <returns>A string containing the description of the rule.</returns>
        public override string ToString() {
            return this.Description;
        }

        /// <summary>
        /// Serves as a hash function for a particular type. System.Object.GetHashCode()
        /// is suitable for use in hashing algorithms and data structures like a hash
        /// table.
        /// </summary>
        /// <returns>A hash code for the current rule.</returns>
        public override int GetHashCode() {
            return this.ToString().GetHashCode();
        }

        protected object GetPropertyValue(DomainObject domainObject) {
            PropertyInfo pi = domainObject.GetType().GetProperty(this.PropertyName);
            return pi.GetValue(domainObject, null);

        }
    }
}
