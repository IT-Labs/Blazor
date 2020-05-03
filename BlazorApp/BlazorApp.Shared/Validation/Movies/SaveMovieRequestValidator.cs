﻿using BlazorApp.Shared.Requests.Movies;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorApp.Shared.Validation.Movies
{
    public class SaveMovieRequestValidator : AbstractValidator<SaveMovieRequest>, IValidator
    {
        public SaveMovieRequestValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Please enter a title")
                .Must(x => x.Length < 200).WithMessage("Title must be up to 200 charactes");

            RuleFor(x => x.ReleaseDate)
                .Must(x => x.HasValue)
                    .WithMessage("Please enter a date")
                .Must(x => x >= new DateTime(1900, 1, 1) && x <= new DateTime(2029, 12, 31))
                    .WithMessage("Please select a date between 1900 and 2029");
        }
    }
}
