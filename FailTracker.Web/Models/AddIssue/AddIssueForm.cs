using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using FailTracker.Core.Domain;
using FailTracker.Web.Infrastructure.ModelMetadata.Attributes;

namespace FailTracker.Web.Models.AddIssue
{
	public class AddIssueForm
	{
		[Required]
		public string Title { get; set; }

		[Required]
		public IssueType Type { get; set; }

		[Required]
		public PointSize Size { get; set; }

		[UIHint("UserDropDown")]
		public Guid? AssignedTo { get; set; }

		[Required]
		[DataType(DataType.MultilineText)]
		public string Body { get; set; }

		[Required]
		[RenderMode(RenderMode.None)]
		public User CurrentUser { get; set; }

		[HiddenInput]
		public Guid TargetProjectID { get; set; }
	}
}