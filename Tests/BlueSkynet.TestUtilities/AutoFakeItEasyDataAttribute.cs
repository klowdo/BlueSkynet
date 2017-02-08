using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoFakeItEasy;
using Ploeh.AutoFixture.NUnit3;
using System;

namespace BlueSkynet.TestUtilities
{
    public class AutoFakeItEasyDataAttribute : AutoDataAttribute
    {
        public AutoFakeItEasyDataAttribute()
            : base(new Fixture().Customize(new BlueSkynetAutoFakeItEasyCustomization()))
        {
        }
    }

    public class BlueSkynetAutoFakeItEasyCustomization : AutoFakeItEasyCustomization
    {
        public new void Customize(IFixture fixture)
        {
            fixture.Build<Guid>().FromFactory(Guid.NewGuid);
            base.Customize(fixture);
        }
    }
}