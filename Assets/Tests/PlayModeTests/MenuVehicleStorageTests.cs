using System.Linq;
using DataModel.Vehicles;
using FluentAssertions;
using Menu.Deck;
using NSubstitute;
using NUnit.Framework;
using UniRx;
using UnityEngine;

namespace Tests.PlayModeTests
{
	[TestFixture]
	public class MenuVehicleStorageTests
	{
		private ICardRepository _repository;
		private IPlacement _placement;
		private IDeckSelection _deckSelection;
		
		[SetUp]
		public void SetUp()
		{
			_repository = Substitute.For<ICardRepository>();
			_placement = Substitute.For<IPlacement>();
			_deckSelection = Substitute.For<IDeckSelection>();
			_placement.Add(null).ReturnsForAnyArgs(new GameObject());
		}

		[Test]
		public void WhenChangingDeck_AndDeckSameAsStorage_ThenMenuStorageShouldDraw0Cards()
		{
			VehicleId[] vehicles = 
			{
				VehicleId.Jeep,
				VehicleId.Ute,
				VehicleId.Van

			};
			var deck = Substitute.For<IVehicleDeck>();
			var storage = Substitute.For<IVehicleStorage>();
			storage.BoughtVehicles.Returns(new ReactiveCollection<VehicleId>(vehicles));
			deck.ActiveVehicles.Returns(new ReactiveCollection<VehicleId>(vehicles));
			var menuStorage = new MenuVehicleStorage(_repository, storage, _placement, _deckSelection);

			menuStorage.DrawCards(deck);
			
			menuStorage.Cards.Should().BeEmpty();
		}

		[Test]
		public void WhenDrawingCards_AndDeckHas3JeepsAndStorageHas5Jeeps_ThenMenuStorageShouldDraw2Jeeps()
		{
			var deck = Substitute.For<IVehicleDeck>();
			var storage = Substitute.For<IVehicleStorage>();
			deck.ActiveVehicles.Returns(new ReactiveCollection<VehicleId>(Enumerable.Repeat(VehicleId.Jeep, 3)));
			storage.BoughtVehicles.Returns(new ReactiveCollection<VehicleId>(Enumerable.Repeat(VehicleId.Jeep, 5)));
			var menuStorage = new MenuVehicleStorage(_repository, storage, _placement, _deckSelection);

			menuStorage.DrawCards(deck);

			menuStorage.Cards.Should().HaveCount(2);
			menuStorage.Cards.Select(card => card.VehicleId).Should().Contain(Enumerable.Repeat(VehicleId.Jeep, 2));
		}

		[Test]
		public void WhenDrawingCardsTwoTimes_AndDeckAreDifferent_ThenMenuStorageShouldHaveCount3AndShouldNotContainSecondDeck()
		{
			var deck1 = Substitute.For<IVehicleDeck>();
			deck1.ActiveVehicles.Returns(new ReactiveCollection<VehicleId>(new[]
			{
				VehicleId.Jeep, VehicleId.Jeep, VehicleId.Jeep
			}));
			var deck2 = Substitute.For<IVehicleDeck>();
			deck2.ActiveVehicles.Returns(new ReactiveCollection<VehicleId>(new[]
			{
				VehicleId.Ute, VehicleId.Ute, VehicleId.Ute
			}));
			var storage = Substitute.For<IVehicleStorage>();
			storage.BoughtVehicles.Returns(new ReactiveCollection<VehicleId>(new[]
			{
				VehicleId.Jeep, VehicleId.Jeep, VehicleId.Jeep,
				VehicleId.Ute, VehicleId.Ute, VehicleId.Ute
			}));
			var menuStorage = new MenuVehicleStorage(_repository, storage, _placement, _deckSelection);

			menuStorage.DrawCards(deck1);
			menuStorage.DrawCards(deck2);
			
			menuStorage.Cards.Should().HaveCount(3);
			menuStorage.Cards.Select(card => card.VehicleId).Should().NotContain(new[]
			{
				VehicleId.Ute, VehicleId.Ute, VehicleId.Ute
			});
		}
	}
}