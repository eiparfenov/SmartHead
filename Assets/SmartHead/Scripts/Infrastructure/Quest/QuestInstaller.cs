using SmartHead.Infrastructure.Quest.Factories;
using SmartHead.Quest;
using SmartHead.Quest.Displays;
using TMPro;
using UnityEngine;
using Zenject;

namespace SmartHead.Infrastructure.Quest
{
    public class QuestInstaller: MonoInstaller
    {
        [SerializeField] private GameObject optionPref;
        [SerializeField] private Transform optionParent;
        [SerializeField] private TextMeshProUGUI mainText;
        [SerializeField] private QuestNode startNode;

        public override void InstallBindings()
        {
            Container
                .BindFactory<QuestNode.Option, OptionDisplay, OptionDisplayFactory>()
                .FromSubContainerResolve()
                .ByNewPrefabInstaller<OptionDisplayInstaller>(optionPref)
                .UnderTransform(optionParent)
                .AsSingle();
            
            Container.Bind<TextMeshProUGUI>().FromInstance(mainText).AsCached().WhenInjectedInto<QuestNodeDisplay>();
            Container.BindInterfacesAndSelfTo<QuestNodeDisplay>().AsCached();

            Container.Bind<QuestNode>().FromInstance(startNode).AsCached();
            Container.BindInterfacesAndSelfTo<QuestManager>().AsCached().NonLazy();
        }
    }
}