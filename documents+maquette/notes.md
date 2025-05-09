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
