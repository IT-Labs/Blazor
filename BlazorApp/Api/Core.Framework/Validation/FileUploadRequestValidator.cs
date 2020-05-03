using Core.Shared.Requests.File;
using Core.Shared.Validation;
using Core.Shared.ValidationCodes;
using Core.Framework.Extensions;
using FluentValidation;

namespace Core.Framework.Validation
{
    public class FileUploadRequestValidator : ValidatorBase<FileUploadRequest>
    {
        public FileUploadRequestValidator()
        {
            RuleFor(x => x.File).Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull()
                    .WithErrorCode(ValidationCodes.FileUpload.Fu002.ToString()).WithMessage(x => $"Please upload a {x.FileUploadType.ToString()}.")
                .Must(x => x.Length > 0)
                    .WithErrorCode(ValidationCodes.FileUpload.Fu002.ToString()).WithMessage(x => $"Please upload a {x.FileUploadType.ToString()}.");
            RuleFor(x => x.Id).NotNull()
                .WithErrorCode(ValidationCodes.Common.Cmn002.ToString()).WithMessage(x => ValidationCodes.Common.Cmn002.GetDescription());
        }
    }
}