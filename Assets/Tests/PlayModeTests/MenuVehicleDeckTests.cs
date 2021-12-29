using System;
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
	public class MenuVehicleDeckTests
	{
		private IPlacement _placement;
		private IVehicleDeck _playerDeck;
		private ICardRepository _repository;
		
		[SetUp]
		public void SetUp()
		{
			_placement = Substitute.For<IPlacement>();
			_playerDeck = Substitute.For<IVehicleDeck>();
			_repository = Substitute.For<ICardRepository>();
			_placement.Add(null).Returns(new GameObject());
			_playerDeck.ActiveVehicles.Returns(new ReactiveCollection<VehicleId>(new[]
			{
				VehicleId.Jeep,
				VehicleId.Ute,
				VehicleId.Van
			}));
		}

		[Test]
		public void WhenReplacingCard_AndMenuDeckIsNotInitialized_ThenExceptionShouldBeThrown()
		{
			var menuDeck = new MenuVehicleDeck(_placement, _playerDeck, _repository);
			ICard card = Substitute.For<ICard>();
			card.VehicleId.Returns(VehicleId.Ute);
			
			Action act = () => menuDeck.ReplaceWith(card, 1);

			act.Should().Throw<Exception>();
		}

		[Test]
		public void WhenReplacingCard_AndUsingPositionAboveDeckSize_ThenExceptionShouldBeThrown()
		{
			var menuDeck = new MenuVehicleDeck(_placement, _playerDeck, _repository);
			menuDeck.Initialize();
			ICard card = Substitute.For<ICard>();
			card.VehicleId.Returns(VehicleId.Jeep);

			Action act = () => menuDeck.ReplaceWith(card, _playerDeck.ActiveVehicles.Count);

			act.Should().Throw<Exception>();
		}

		[Test]
		public void WhenReplacingCards_AndAllCardsToReplaceAreUtes_ThenPlayerDeckShouldOnlyContainUtes()
		{
			var playerDeck = Substitute.For<IVehicleDeck>();
			playerDeck.ActiveVehicles.Returns(new ReactiveCollection<VehicleId>(new[]
			{
				VehicleId.Jeep,
				VehicleId.Ute,
				VehicleId.Van
			}));
			var menuDeck = new MenuVehicleDeck(_placement, playerDeck, _repository);
			menuDeck.Initialize();
			ICard card = Substitute.For<ICard>();
			card.VehicleId.Returns(VehicleId.Ute);
			
			menuDeck.ReplaceWith(card, 0);
			menuDeck.ReplaceWith(card, 1);
			menuDeck.ReplaceWith(card, 2);

			playerDeck.ActiveVehicles.Should().Contain(new[]
			{
				VehicleId.Ute,
				VehicleId.Ute,
				VehicleId.Ute
			});
		}

		[Test]
		public void WhenInitializingMenuDeck_AndPlayerDeckHas3Vehicles_ThenMenuDeckShouldContainSameCardIds()
		{
			var playerDeck = Substitute.For<IVehicleDeck>();
			playerDeck.ActiveVehicles.Returns(new ReactiveCollection<VehicleId>(new[]
			{
				VehicleId.Jeep,
				VehicleId.Ute,
				VehicleId.Van
			}));
			var menuDeck = new MenuVehicleDeck(_placement, playerDeck, _repository);
			
			menuDeck.Initialize();

			menuDeck.Cards.Select(card => card.VehicleId).Should().Contain(new[]
			{
				VehicleId.Jeep,
				VehicleId.Ute,
				VehicleId.Van

			});
		}
	}
}