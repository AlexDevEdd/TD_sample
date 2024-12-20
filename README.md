___
## Немного слов о решении
### Поставленные задачи выполнены в полном объёме.
#### Используемый стек:
- Zenject
- UniTask
- Addressables
- R3(Rx)
- PrimeTween (самый производительный)
____

ECS не стал использовать так как задача была сделать простую архитектуру, в случае с ECS не каждый разработчик знает этот подход.
В данном проекте не стал использовать Jobs и Burst, так как выгода будет крайне мала, при текущем количестве сущностей.

[Мини прототип на ECS](https://github.com/AlexDevEdd/Ecs_Auto_RTS) описание ТЗ было доработано.
[Видео геймплея](https://drive.google.com/file/d/1BMMEkMVkLOSy9s_WS-A938hVh_5qq4ds/view?usp=drive_link)

___
По оптимизации было предпринято:
- Addressables
- пулы объектов
- настроена матрица коллизий
- минимизированы апдейты
- убран копонент аниамции с кристала в пользу кодовой анимации.
- кеширование данных
- минимизированы аллокации
- убраны меш коллайдеры, где они не нужны.
___
В проекте намерено используется несколько подходов для реализации игровых сущностей:
- попроще, что поймут аж самые юные разработчики (реализация юнитов) 
- другой вариант тоже простой, но нужны уже знания DI (реализация башен)).

Классические варианты реализации ИИ так же не стал применять.


В этом репозитории [FPS](https://github.com/AlexDevEdd/UnityShowcase/tree/DEV), например реализация ИИ через Behaviour Tree
___
Мне больше импонирует ***Data oriented design***, последнее время как раз его и использую, так как очень легко в рантайме добавлять новое поведение или новые данные игровым сущностям, а в играх как раз очень часто объекты это требуют.
В этом помогает ___процедурное___ и ___реактивное___ программирование.
___
В проект добавлены несколько модулей, которые часто мигрируют по проектам:
+ Жизненный цикл игры (zenject специально подредактирован чтобы вклиниться в зенжектовские апдейты)
+ Дерево загрузки (чёткий пайплайн загрузки приложения важен)
+ Менеджмент ассетов
___
Интеграция модулей практически не занимает времени. Простые в использовании. 
___
