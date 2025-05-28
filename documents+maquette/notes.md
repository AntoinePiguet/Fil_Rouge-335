04.04.25
le xaml est nouveau mais ressemble au html car =>balises
4 types de conception

- absolute => pas résponsive mais on peut empiler les éléments
- stack => empilé, choicir si stack horizontal/vertical
- griles => définir une grille ou l'on va positionner ses éléments, on doit définir le nombre de lignes et colonnes
- flex => éléments disposés automatiquement, ressemble au grid mais sans définir le nbmr de colonnes ou lignes et s'adapte auto a la taille de l'écran. grille automatique

navigation:
cette nav est propre à MAUI à définir dans le AppShell

- flyout
- contentPage = sous-naviagtion pas dans AppShell=> système de page stacking qui permet de revenir à la dernière page avec des pop() et push()
- tabPage = naviagtion générale dans AppShell=> des onglets pour naviguer entre les pages
- flyout = navigation en slide dans un AppShell=> permet de slide d'une page à l'autre

**Balise Flyout où on mets des tabs, pas l'inverse**

les questions

- 1. le nom de l'application et les 2 types de navigations
     - AppShell, tabulation, flyout
- 2. avec une navigation standard comment naviguer d'une page à l'autre
     - avec push() et pop()
- 3. citer les 4 layouts de base et leur comportement
     3.1. absolute => pas résponsive mais on peut empiler les éléments
     3.2. stack => empilé, choicir si stack horizontal/vertical
     3.3. griles => définir une grille ou l'on va positionner ses éléments, on doit définir le nombre de lignes et colonnes
     3.4. flex => éléments disposés automatiquement, ressemble au grid mais sans définir le nbmr de colonnes ou lignes et s'adapte auto a la taille de l'écran. grille automatique

.

11.04.25

mvvm = une logique particulière:
séparer la logique métier de l'interface et des données
c'est le lien entre ces différents aspects de l'app.
le but est de séparer l'intérfaçage, l'algorithmie et la db

pratique:

aller dans Outils/ gestio package NuGet/ gérer
installer le communityToolkit.mvvm

utiliser les méthodes et classes observables dans le ViewModel créé apres avoir installé le toolkit
utiliser les attributs Binding pour lier les observables avec le reste du code

**Récap de toutes les questions sur MAUI**

1. **Que veut dire l'acronyme MAUI ?**  
   Multi-platform Application User Interface

2. **Comment se fait-il que C# fonctionne sur Android ?**  
   Grâce à un runtime qui compile le code pour qu’il s’exécute comme une application Android.

3. **Comment tester une application MAUI Android développée sur Windows alors que nous n'avons pas d'appareil mobile ?**  
   En utilisant un émulateur Android.

4. **Citez 3 alternatives à MAUI pour le développement mobile.**  
   Développement natif dédié, frameworks hybrides comme React Native, Kotlin Multiplatform, ou encore des technologies comme WebAssembly.

5. **Citez le type d'application qui permet d'avoir les 2 options de navigation principales, citez les deux options et illustrez leur rendu.**  
   Type : Shell  
   Options : Navigation par onglets (Tab) et menu latéral (Flyout)

6. **Avec une navigation standard, sans AppShell, comment naviguer entre les pages ?**  
   On utilise des `ContentPage` avec une `NavigationPage`, et les méthodes `Push` et `Pop` pour la navigation.

7. **Citez les 4 Layouts de base et décrivez leur comportement principal.**

   - **Grid** : organise les éléments dans une grille définie par des lignes et des colonnes
   - **FlexLayout** : s’adapte de manière responsive à l’espace disponible
   - **StackLayout** : aligne les éléments verticalement ou horizontalement
   - **AbsoluteLayout** : positionne les éléments avec des coordonnées exactes

8. **Que signifie MVVM ?**  
   Model-View-ViewModel : une architecture qui sépare les données (Model), l’interface utilisateur (View) et la logique de liaison (ViewModel)

9. **À quoi sert la notation `[RelayCommand]` et d'où vient-elle ?**  
   Elle permet de lier une méthode du ViewModel à une interaction de la vue, tout comme `[ObservableProperty]` relie une variable. Elle vient de la bibliothèque `CommunityToolkit.Mvvm`.

10. **Comment faire pour qu’un `Label` affiche dynamiquement la valeur d’un entier ?**  
    En liant la propriété `Text` du `Label` à une propriété marquée `[ObservableProperty]` dans le ViewModel.

11. **Citer une alternative à MVVM ?**  
    Le code-behind, ou des approches comme React ou Blazor.

12. **Comment faire pour que le contenu d’une liste soit conservé après un redémarrage de l’application ?**  
    Il faut stocker les données dans un système de persistance comme une base de données SQLite.

13. **À quelle fréquence les données de l’accéléromètre sont-elles transmises à l’application ?**  
    Cela dépend du paramétrage : Default = 200 ms, UI = 60 ms, Game = 20 ms, Fastest = 5 ms

14. **L’accéléromètre détecte les mouvements selon quels axes ?**  
    Les axes X, Y et Z.

15. **En quoi les capteurs peuvent-ils avoir un impact négatif sur le téléphone ?**  
    Ils consomment des ressources, notamment la batterie, ce qui peut réduire l’autonomie de l’appareil.
