// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Threading.Tasks;

using Microsoft.Templates.Core;
using Microsoft.Templates.Core.Extensions;
using Microsoft.TemplateEngine.Abstractions;

using Xunit;
using Microsoft.Templates.Core.Gen;

namespace Microsoft.Templates.Test.Build.Uwp
{
    [Collection("BuildTemplateTestCollection")]
    public class BuildCaliburnMicroProjectTests : BaseGenAndBuildTests
    {
        public BuildCaliburnMicroProjectTests(BuildTemplatesTestFixture fixture)
            : base(fixture, null, Frameworks.CaliburnMicro)
        {
        }

        [Theory]
        [MemberData(nameof(BaseGenAndBuildTests.GetProjectTemplatesForBuild), Frameworks.CaliburnMicro, ProgrammingLanguages.CSharp, Platforms.Uwp)]
        [Trait("ExecutionSet", "BuildCaliburnMicro")]
        [Trait("ExecutionSet", "_Full")]
        [Trait("Type", "BuildProjects")]
        public async Task Build_EmptyProject_InferConfig_UwpAsync(string projectType, string framework, string platform, string language)
        {
            var context = new UserSelectionContext(language, platform)
            {
                ProjectType = projectType,
                FrontEndFramework = framework
            };

            var (projectName, projectPath) = await GenerateEmptyProjectAsync(context);

            // Don't delete after build test as used in inference test, which will then delete.
            AssertBuildProject(projectPath, projectName, platform, deleteAfterBuild: false);

            EnsureCanInferConfigInfo(projectType, framework, platform, projectPath);
        }

        [Theory]
        [MemberData(nameof(BaseGenAndBuildTests.GetProjectTemplatesForBuild), Frameworks.CaliburnMicro, ProgrammingLanguages.CSharp, Platforms.Uwp)]
        [Trait("ExecutionSet", "BuildCaliburnMicro")]
        [Trait("ExecutionSet", "_Full")]
        [Trait("Type", "BuildAllPagesAndFeatures")]
        [Trait("Type", "BuildRandomNames")]
        public async Task Build_All_ProjectNameValidation_G1_UwpAsync(string projectType, string framework, string platform, string language)
        {
            bool templateSelector(ITemplateInfo t) => t.GetTemplateType().IsItemTemplate()
                && (t.GetProjectTypeList().Contains(projectType) || t.GetProjectTypeList().Contains(All))
                && (t.GetFrontEndFrameworkList().Contains(framework) || t.GetFrontEndFrameworkList().Contains(All))
                && t.GetPlatform() == platform
                && !excludedTemplates_Uwp_Group2.Contains(t.GroupIdentity)
                && !t.GetIsHidden();

            var projectName = $"{ShortProjectType(projectType)}{CharactersThatMayCauseProjectNameIssues()}G1{ShortLanguageName(language)}";

            var context = new UserSelectionContext(language, platform)
            {
                ProjectType = projectType,
                FrontEndFramework = framework
            };

            var projectPath = await AssertGenerateProjectAsync(projectName, context, templateSelector, BaseGenAndBuildFixture.GetRandomName);

            AssertBuildProject(projectPath, projectName, platform);
        }

        [Theory]
        [MemberData(nameof(BaseGenAndBuildTests.GetProjectTemplatesForBuild), Frameworks.CaliburnMicro, ProgrammingLanguages.CSharp, Platforms.Uwp)]
        [Trait("ExecutionSet", "BuildCaliburnMicro")]
        [Trait("ExecutionSet", "_Full")]
        [Trait("Type", "BuildAllPagesAndFeatures")]
        [Trait("Type", "BuildRandomNames")]
        public async Task Build_All_ProjectNameValidation_G2_UwpAsync(string projectType, string framework, string platform, string language)
        {
            bool templateSelector(ITemplateInfo t) => t.GetTemplateType().IsItemTemplate()
                && (t.GetProjectTypeList().Contains(projectType) || t.GetProjectTypeList().Contains(All))
                && (t.GetFrontEndFrameworkList().Contains(framework) || t.GetFrontEndFrameworkList().Contains(All))
                && t.GetPlatform() == platform
                && !excludedTemplates_Uwp_Group1.Contains(t.GroupIdentity)
                && !t.GetIsHidden();

            var projectName = $"{ShortProjectType(projectType)}{CharactersThatMayCauseProjectNameIssues()}G2{ShortLanguageName(language)}";

            var context = new UserSelectionContext(language, platform)
            {
                ProjectType = projectType,
                FrontEndFramework = framework
            };

            var projectPath = await AssertGenerateProjectAsync(projectName, context, templateSelector, BaseGenAndBuildFixture.GetRandomName);

            AssertBuildProject(projectPath, projectName, platform);
        }

        [Theory]
        [MemberData(nameof(BaseGenAndBuildTests.GetProjectTemplatesForBuild), Frameworks.CaliburnMicro, ProgrammingLanguages.CSharp, Platforms.Uwp)]
        [Trait("ExecutionSet", "Minimum")]
        [Trait("ExecutionSet", "BuildCaliburnMicro")]
        [Trait("ExecutionSet", "_Full")]
        [Trait("Type", "CodeStyle")]
        public async Task BuildAndTest_All_CheckWithStyleCop_G1_UwpAsync(string projectType, string framework, string platform, string language)
        {
            bool templateSelector(ITemplateInfo t) => t.GetTemplateType().IsItemTemplate()
                && ((t.GetProjectTypeList().Contains(projectType) || t.GetProjectTypeList().Contains(All))
                && (t.GetFrontEndFrameworkList().Contains(framework) || t.GetFrontEndFrameworkList().Contains(All))
                && t.GetPlatform() == platform
                && !t.GetIsHidden()
                && !excludedTemplates_Uwp_Group2.Contains(t.GroupIdentity))
                || t.Identity == "wts.Feat.StyleCop";

            var projectName = $"{ShortProjectType(projectType)}{ShortProjectType(framework)}AllStyleCopG1";

            var context = new UserSelectionContext(language, platform)
            {
                ProjectType = projectType,
                FrontEndFramework = framework
            };

            var projectPath = await AssertGenerateProjectAsync(projectName, context, templateSelector, BaseGenAndBuildFixture.GetDefaultName, false);

            AssertBuildProjectThenRunTests(projectPath, projectName, platform);
        }

        [Theory]
        [MemberData(nameof(BaseGenAndBuildTests.GetProjectTemplatesForBuild), Frameworks.CaliburnMicro, ProgrammingLanguages.CSharp, Platforms.Uwp)]
        [Trait("ExecutionSet", "Minimum")]
        [Trait("ExecutionSet", "MinimumCaliburnMicro")]
        [Trait("ExecutionSet", "_CIBuild")]
        [Trait("ExecutionSet", "_Full")]
        [Trait("Type", "CodeStyle")]
        public async Task BuildAndTest_All_CheckWithStyleCop_G2_UwpAsync(string projectType, string framework, string platform, string language)
        {
            bool templateSelector(ITemplateInfo t) => t.GetTemplateType().IsItemTemplate()
                && ((t.GetProjectTypeList().Contains(projectType) || t.GetProjectTypeList().Contains(All))
                && (t.GetFrontEndFrameworkList().Contains(framework) || t.GetFrontEndFrameworkList().Contains(All))
                && t.GetPlatform() == platform
                && !t.GetIsHidden()
                && !excludedTemplates_Uwp_Group1.Contains(t.GroupIdentity))
                || t.Identity == "wts.Feat.StyleCop";

            var projectName = $"{ShortProjectType(projectType)}{ShortProjectType(framework)}AllStyleCopG2";

            var context = new UserSelectionContext(language, platform)
            {
                ProjectType = projectType,
                FrontEndFramework = framework
            };

            var projectPath = await AssertGenerateProjectAsync(projectName, context, templateSelector, BaseGenAndBuildFixture.GetDefaultName, true);

            AssertBuildProjectThenRunTests(projectPath, projectName, platform);
        }

        [Theory]
        [MemberData(nameof(BaseGenAndBuildTests.GetProjectTemplatesForBuild), Frameworks.CaliburnMicro, ProgrammingLanguages.CSharp, Platforms.Uwp)]
        [Trait("ExecutionSet", "BuildCaliburnMicro")]
        [Trait("ExecutionSet", "_Full")]
        [Trait("Type", "BuildRightClick")]
        public async Task Build_Empty_AddRightClick_UwpAsync(string projectType, string framework, string platform, string language)
        {
            var projectName = $"{ShortProjectType(projectType)}AllRC";

            var context = new UserSelectionContext(language, platform)
            {
                ProjectType = projectType,
                FrontEndFramework = framework
            };

            var projectPath = await AssertGenerateRightClickAsync(projectName, context, true);

            AssertBuildProject(projectPath, projectName, platform);
        }

        [Theory]
        [MemberData(nameof(BaseGenAndBuildTests.GetPageAndFeatureTemplatesForBuild), Frameworks.CaliburnMicro, ProgrammingLanguages.CSharp, Platforms.Uwp, "")]
        [Trait("ExecutionSet", "BuildOneByOneCaliburnMicro")]
        [Trait("ExecutionSet", "_OneByOne")]
        [Trait("Type", "BuildOneByOneCaliburnMicro")]
        public async Task Build_Caliburn_OneByOneItems_UwpAsync(string itemName, string projectType, string framework, string platform, string itemId, string language)
        {
            var context = new UserSelectionContext(language, platform)
            {
                ProjectType = projectType,
                FrontEndFramework = framework
            };

            var (ProjectPath, ProjecName) = await AssertGenerationOneByOneAsync(itemName, context, itemId, false);

            AssertBuildProject(ProjectPath, ProjecName, platform);
        }
    }
}
