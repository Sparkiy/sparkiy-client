namespace SparkiyClient.UILogic.Services
{
	public interface IProjectPlayStateManagment
	{
		void PlayProject();

		void PauseProject();

	    void StopProject();

		void RestartProject();

		void TakeScreenshot();


		bool IsInitialized { get; }

		bool IsPlaying { get; }

		bool IsPause { get; }


		event ProjectPlayStateEventHandler OnStateChanged;
	}
}