namespace AI.StateMachine.Common
{
	public interface ITransferReceiver<in TPackage> : ITransferReceiver
	{
		void Accept(TPackage package);
	}

	public interface ITransferReceiver
	{
	}
}