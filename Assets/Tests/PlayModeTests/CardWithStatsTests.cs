using System.Collections;
using FluentAssertions;
using Menu.Cards;
using NSubstitute;
using NUnit.Framework;
using Services.Instantiate;
using Sources.Core.Tests.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.TestTools;

namespace Tests.PlayModeTests
{
	[TestFixture]
	public class CardWithStatsTests : ZenjectUnitTestFixture
	{
		private const ICard Any = null;
		private const string AssetPath = "Assets/Bundles/Vehicles/Prefabs/Cards/VehicleCard.prefab";

		[SetUp]
		public void InstallBindings()
		{
			Container
				.Bind<ICardStatsRepository>()
				.FromInstance(Substitute.For<ICardStatsRepository>())
				.AsSingle();

			Container
				.Bind<IAsyncInstantiation>()
				.To<DiInstantiation>()
				.AsSingle();
		}

		[UnityTest]
		public IEnumerator WhenGameIsStarting_AndForceIs5AndFuelIs4_ThenTextElementsShouldHaveSameValues()
		{
			// Arrange
			Container
				.Resolve<ICardStatsRepository>()
				.StatsOf(Any)
				.ReturnsForAnyArgs(new VerboseVehicleStats
				{
					Force = 5,
					Fuel = 4
				});

			AsyncOperationHandle<GameObject> resource = Addressables
				.LoadAssetAsync<GameObject>(AssetPath);
			yield return resource;
			CardWithStats cardWithStats = Container.InstantiatePrefabForComponent<CardWithStats>(resource.Result);

			// Act
			yield return new WaitForEndOfFrame();

			// Assert
			cardWithStats.TestField<TextMeshProUGUI>("ForceText").text.Should().Be("5");
			cardWithStats.TestField<TextMeshProUGUI>("FuelText").text.Should().Be("4");
		}
	}
}