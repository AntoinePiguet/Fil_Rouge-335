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
