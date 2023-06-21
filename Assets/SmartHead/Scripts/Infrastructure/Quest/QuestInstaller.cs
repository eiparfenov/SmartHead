using SmartHead.Infrastructure.Quest.Factories;
using SmartHead.Quest;
using SmartHead.Quest.Components;
using SmartHead.Quest.Displays;
using SmartHead.Quest.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace SmartHead.Infrastructure.Quest
{
    public class QuestInstaller: MonoInstaller
    {
        [Header("Node")]
        [SerializeField] private NodeBaseQuestProvider questProvider;

        [Header("Option")]
        [SerializeField] private GameObject optionPref;
        [SerializeField] private GridLayoutGroup optionParent;
        [SerializeField] private TextMeshProUGUI mainText;

        [Header("Start")] 
        [SerializeField] private GameObject startScreen;
        [SerializeField] private Button startButton;
        
        [Header("Finish")] 
        [SerializeField] private GameObject finishScreen;
        [SerializeField] private TextMeshProUGUI finishText;
        [SerializeField] private Image finishImage;

        public override void InstallBindings()
        {
            Container
                .BindFactory<IOptionModel, OptionDisplay, OptionDisplayFactory>()
                .FromSubContainerResolve()
                .ByNewPrefabInstaller<OptionDisplayInstaller>(optionPref)
                .UnderTransform(optionParent.transform)
                .AsSingle();

            Container.Bind<GameObject>().FromInstance(startScreen).AsCached().WhenInjectedInto<QuestStartDisplay>();
            Container.Bind<Button>().FromInstance(startButton).AsCached().WhenInjectedInto<QuestStartDisplay>();
            Container.BindInterfacesAndSelfTo<QuestStartDisplay>().AsCached();
            
            Container.Bind<TextMeshProUGUI>().FromInstance(mainText).AsCached().WhenInjectedInto<QuestNodeDisplay>();
            Container.Bind<GridLayoutGroup>().FromInstance(optionParent).AsCached().WhenInjectedInto<QuestNodeDisplay>();
            Container.BindInterfacesAndSelfTo<QuestNodeDisplay>().AsCached();

            Container.Bind<GameObject>().FromInstance(finishScreen).AsCached().WhenInjectedInto<QuestFinishDisplay>();
            Container.Bind<TextMeshProUGUI>().FromInstance(finishText).AsCached().WhenInjectedInto<QuestFinishDisplay>();
            Container.Bind<Image>().FromInstance(finishImage).AsCached().WhenInjectedInto<QuestFinishDisplay>();
            Container.BindInterfacesAndSelfTo<QuestFinishDisplay>().AsCached();
            
            Container.BindInterfacesTo<NodeBaseQuestProvider>().FromInstance(questProvider).AsCached();
            
            Container.BindInterfacesAndSelfTo<QuestManager>().AsCached().NonLazy();
        }
    }
}