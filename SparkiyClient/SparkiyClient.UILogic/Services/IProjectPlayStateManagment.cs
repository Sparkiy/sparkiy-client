namespace SparkiyClient.UILogic.Services
{
	public interface IProjectPlayStateManagment
	{
		void PlayProject();

		void PauseProject();

		void RestartProject();

		void TakeScreenshot();


		bool IsInitialized { get; }

		bool IsPlaying { get; }

		bool IsPause { get; }
	}
}