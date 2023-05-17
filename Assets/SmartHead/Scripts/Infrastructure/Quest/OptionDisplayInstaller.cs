using SmartHead.Quest;
using SmartHead.Quest.Displays;
using TMPro;
using UnityEngine.UI;
using Zenject;

namespace SmartHead.Infrastructure.Quest
{
    public class OptionDisplayInstaller: Installer<QuestNode.Option, OptionDisplayInstaller>
    {
        private readonly QuestNode.Option _model;

        public OptionDisplayInstaller(QuestNode.Option model)
        {
            _model = model;
        }

        public override void InstallBindings()
        {
            Container.BindInstance(_model).AsCached();
            Container.Bind<Button>().FromComponentOnRoot().AsCached();
            Container.Bind<TextMeshProUGUI>().FromComponentInHierarchy().AsCached();

            Container.BindInterfacesAndSelfTo<OptionDisplay>().AsCached();
        }
    }
}