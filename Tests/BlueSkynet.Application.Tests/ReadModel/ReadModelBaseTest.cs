using BlueSkynet.Domain.Data;
using BlueSkynet.Domain.Models;
using BlueSkynet.Domain.Models.ServiceBus;
using BlueSkynet.Domain.Models.ServiceBus.Models;
using BlueSkynet.Infrastructure.ReadModels;
using BlueSkynet.TestUtilities;
using LiteDB;
using NUnit.Framework;
using Ploeh.AutoFixture.NUnit3;

namespace BlueSkynet.Application.Tests.ReadModel
{
    internal class ReadModelBaseTest
    {
        [Theory, AutoFakeItEasyData]
        public void NameOfViewList_BeCollectiontionName(
            [Frozen]LiteDatabase db,
            DummyDtoListView sut)
        {
            db.GetCollection<DummyDto>(nameof(DummyDto)).Insert(new DummyDto());
            Assert.IsNotEmpty(db.GetCollectionNames());
            Assert.True(db.CollectionExists(nameof(DummyDto)));
        }
    }

    internal class DummyDto : Entity
    {
    }

    internal class DummyDtoListView : ReadModelBase<DummyDto>
    {
        public DummyDtoListView(IDataContext db) : base(db)
        {
        }
    }
}