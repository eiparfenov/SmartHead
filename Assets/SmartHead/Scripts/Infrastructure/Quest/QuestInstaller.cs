using SmartHead.Infrastructure.Quest.Factories;
using SmartHead.Quest;
using SmartHead.Quest.Displays;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace SmartHead.Infrastructure.Quest
{
    public class QuestInstaller: MonoInstaller
    {
        [Header("Option")]
        [SerializeField] private GameObject optionPref;
        [SerializeField] private Transform optionParent;
        
        [Space]
        [SerializeField] private Button startButton;
        
        [Header("Node")]
        [SerializeField] private TextMeshProUGUI mainText;
        [SerializeField] private QuestNode startNode;

        [Header("Finish")] 
        [SerializeField] private TextMeshProUGUI finishText;
        [SerializeField] private Image finishImage;

        public override void InstallBindings()
        {
            Container
                .BindFactory<QuestNode.Option, OptionDisplay, OptionDisplayFactory>()
                .FromSubContainerResolve()
                .ByNewPrefabInstaller<OptionDisplayInstaller>(optionPref)
                .UnderTransform(optionParent)
                .AsSingle();
            
            Container.Bind<Button>().FromInstance(startButton).AsCached().WhenInjectedInto<QuestStartDisplay>();
            Container.BindInterfacesAndSelfTo<QuestStartDisplay>().AsCached();
            
            Container.Bind<TextMeshProUGUI>().FromInstance(mainText).AsCached().WhenInjectedInto<QuestNodeDisplay>();
            Container.BindInterfacesAndSelfTo<QuestNodeDisplay>().AsCached();
            
            Container.Bind<TextMeshProUGUI>().FromInstance(finishText).AsCached().WhenInjectedInto<QuestFinishDisplay>();
            Container.Bind<Image>().FromInstance(finishImage).AsCached().WhenInjectedInto<QuestFinishDisplay>();
            Container.BindInterfacesAndSelfTo<QuestFinishDisplay>().AsCached();
            
            Container.Bind<QuestNode>().FromInstance(startNode).AsCached();
            
            Container.BindInterfacesAndSelfTo<QuestManager>().AsCached().NonLazy();
        }
    }
}