using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace practise.BookStoreApp.Helpers
{
    public class MyCustomValidationAttributes : ValidationAttribute
    {
        public MyCustomValidationAttributes(string text)
        {
            Text = text;
        }

        public string Text { get; set; }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                string bookName = value.ToString();
                if (bookName.Contains(Text))
                {
                    return ValidationResult.Success;
                }

            }
            return new ValidationResult(ErrorMessage ?? "value is empty");
        }
    }
}
