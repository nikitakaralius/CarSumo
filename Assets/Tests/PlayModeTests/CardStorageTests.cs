using System.Linq;
using DataModel.Vehicles;
using FluentAssertions;
using Menu.Cards;
using NSubstitute;
using NUnit.Framework;
using UniRx;
using UnityEngine;

namespace Tests.PlayModeTests
{
	[TestFixture]
	public class CardStorageTests
	{
		private IStorageSelection _selection;
		private IPlacement _placement;
		private ICardViewRepository _repository;
		
		[SetUp]
		public void SetUp()
		{
			_selection = Substitute.For<IStorageSelection>();
			_placement = Substitute.For<IPlacement>();
			_repository = Substitute.For<ICardViewRepository>();
			_placement.AddFromPrefab(null).ReturnsForAnyArgs(new GameObject());
		}

		[Test]
		public void WhenDrawing_AndStorageConsistsOfDeck_ThenNoCardsShouldBeDrawn()
		{
			// Arrange
			Vehicle[] vehicles = 
			{
				Vehicle.Jeep,
				Vehicle.Jeep,
				Vehicle.Jeep
			};
			IVehicleStorage vehicleStorage = Substitute.For<IVehicleStorage>();
			IVehicleDeck deck = Substitute.For<IVehicleDeck>();
			vehicleStorage.BoughtVehicles.Returns(new ReactiveCollection<Vehicle>(vehicles));
			deck.ActiveVehicles.Returns(new ReactiveCollection<Vehicle>(vehicles));
			var cardStorage = new CardStorage(_selection, _placement, _repository);

			// Act
			cardStorage.Draw(vehicleStorage, deck);

			// Assert
			cardStorage.Cards.Should().HaveCount(0);
		}

		[Test]
		public void WhenDrawingMultipleTimes_AndDeckAndStorageAreDifferent_ThenPreviousCardsShouldBeCleared()
		{
			// Arrange
			IVehicleStorage vehicleStorage = Substitute.For<IVehicleStorage>();
			IVehicleDeck deck = Substitute.For<IVehicleDeck>();
			vehicleStorage.BoughtVehicles.Returns(new ReactiveCollection<Vehicle>(new[] 
			{
				Vehicle.Jeep,
				Vehicle.Jeep,
				Vehicle.Jeep,
				Vehicle.Ute,
				Vehicle.Wagon,
				Vehicle.Wagon
			}));
			deck.ActiveVehicles.Returns(new ReactiveCollection<Vehicle>(new[] 
			{
				Vehicle.Jeep,
				Vehicle.Jeep,
				Vehicle.Jeep
			}));
			var cardStorage = new CardStorage(_selection, _placement, _repository);
			
			// Act
			cardStorage.Draw(vehicleStorage, deck);
			cardStorage.Draw(vehicleStorage, deck);

			// Assert
			cardStorage.Cards.Should().HaveCount(deck.ActiveVehicles.Count);
		}

		[Test]
		public void WhenDrawing_AndDeckHas3JeepsAndStorageHas5Jeeps_Then2JeepsShouldBeDrawn()
		{
			// Arrange
			IVehicleStorage vehicleStorage = Substitute.For<IVehicleStorage>();
			IVehicleDeck deck = Substitute.For<IVehicleDeck>();
			deck.ActiveVehicles.Returns(new ReactiveCollection<Vehicle>(Enumerable.Repeat(Vehicle.Jeep, 3)));
			vehicleStorage.BoughtVehicles.Returns(new ReactiveCollection<Vehicle>(Enumerable.Repeat(Vehicle.Jeep, 5)));
			var cardStorage = new CardStorage(_selection, _placement, _repository);
			
			// Act
			cardStorage.Draw(vehicleStorage, deck);
			cardStorage.Draw(vehicleStorage, deck);

			// Assert
			cardStorage.Cards.Should().HaveCount(2);
			cardStorage.Cards.Select(x => x.Vehicle).Should().Contain(Enumerable.Repeat(Vehicle.Jeep, 2));
		}
	}
}