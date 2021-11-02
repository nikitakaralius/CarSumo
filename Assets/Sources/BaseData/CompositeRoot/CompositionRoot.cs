using System.Threading.Tasks;
using Sirenix.OdinInspector;

namespace BaseData.CompositeRoot.Common
{
	public abstract class CompositionRoot : SerializedMonoBehaviour
	{
		public abstract Task ComposeAsync();
	}
}