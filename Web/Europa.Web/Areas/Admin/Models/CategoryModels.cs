using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Europa.Web.Areas.Admin.Models
{
    public class DashboardPageModel
    {
        public IEnumerable<CategoryViewModel> Categories { get; set; }
    }
    
    public class CategoryViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int PodcastCount { get; set; }
    }

    public class CategoryPageModel
    {
        public CategoryViewModel Category { get; set; }
        public IEnumerable<PodcastViewModel> Podcasts { get; set; }
    }

    public class CategoryCreateModel
    {
        [Display(Description = "Category Name")]
        public string Name { get; set; }
    }

    public class CategoryEditModel
    {
        public Guid Id { get; set; }

        [Display(Description = "Category Name")]
        public string Name { get; set; }
    }

    public class CategoryCreateModelValidator : AbstractValidator<CategoryCreateModel>
    {
        public CategoryCreateModelValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .MinimumLength(5)
                .MaximumLength(50);
        }
    }
}