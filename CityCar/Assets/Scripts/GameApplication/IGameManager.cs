public interface IGameManager
{

    GameFlowData FlowData { get; set; }
    void ManagerInit();
    void ManagerDispose();
}