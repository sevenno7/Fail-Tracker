﻿using System;
using FailTracker.IntegrationTests.Contexts;
using FailTracker.Web.Controllers;
using FailTracker.Web.Models.ProjectAdministration;
using NUnit.Framework;
using SpecsFor;
using SpecsFor.Mvc;
using Should;
using SpecsFor.Mvc.SeleniumExtensions;
using SpecsFor.Mvc.Smtp;

namespace FailTracker.IntegrationTests.ProjectManagement
{
	public class InviteMemberSpecs
	{
		public class when_inviting_a_user_to_a_project : SpecsFor<MvcWebApp>
		{
			private string _projectId;

			protected override void Given()
			{
				Given(new ProjectExists("Dummy Project"));				
				base.Given();

				SUT.NavigateTo<ProjectAdministrationController>(c => c.Index());
				_projectId = SUT.FindDisplayFor<ProjectListViewModel>().DisplayFor(m => m.Summaries[0].ID).Value();
			}

			protected override void When()
			{
				SUT.FindLinkTo<ProjectAdministrationController>(c => c.InviteMember(new Guid(_projectId))).ClickButton();

				SUT.FindFormFor<InviteMemberForm>()
					.Field(f => f.EmailAddress).SetValueTo("other@user.com")
					.Submit();

				SUT.NavigateTo<ProjectAdministrationController>(c => c.Index());
			}

			[Test]
			public void then_it_adds_the_user_to_the_project()
			{
				SUT.FindDisplayFor<ProjectListViewModel>()
					.DisplayFor(m => m.Summaries[0].Members).Text.ShouldEqual("2");
			}

			[Test]
			public void then_it_sends_an_email_to_that_user()
			{
				SUT.Mailbox().MailMessages[0].To[0].Address.ShouldEqual("other@user.com");
			}
		}
	}
}