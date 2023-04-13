# Simple Game Engine

Arquitetura baseada no padrão Component para escalabilidade horizontal baseada em composição. A classe Game é a principal que deve ser instanciada com um subsistema de renderização. Após isso, o jogo deve ser iniciado pelo método Start, responsável por carregar e instanciar os dados do jogo, e depois executado pelo método Run, que inicia o laço principal do jogo até que o método Stop seja invocado.

O jogo é composto por diferentes entidades e cada uma tendo múltiplos componentes. Cada componente deve ser responsável por uma funcionalidade específica da entidade, por exemplo, um para manipulação de entrada, outro para renderização, outro para sons e efeitos, e assim por diante. O objetivo é que a lógica de cada componente seja independente dos demais.

Desse modo, cada componente pode estar associado a um ou mais sistemas representados pela classe SubSystem. Cada sistema é responsável por coordenar os componentes registrados, por exemplo, o sistema de física deve coordenar o movimento e colisão dos componentes. Assim, o jogo será composto por múltiplos sistemas responsáveis por características específicas dos jogo como física, inteligência artificial, música, entre outros.

As classes SubSystem e Component devem ser especializadas dando origem a novos sistemas aumentando as capacidades da máquina do jogo. Atualmente, somente o sistema de entrada de dados através do console está implementado. O trecho de código abaixo exemplifica o uso do arquitetura:


# Game Powers

Simple ARPG with rogue like game elements:
- [ ] Turn Based (Active Time Battle);
- [ ] Procedural Map Generation;
- [ ] Permanent Death;
- [ ] High difficulty;
- [ ] Powers Customization (like Daemon Supers[^1]);
- [ ] Rare and Champion Monsters (like Diablo 3[^2]);
- [ ] Puzzles (like Zelda[^3], Pokemon[^4], video games[^5], Goof a Troop[^16] and RPG[^6]);
- [ ] Races, Classes and Weapons;
- [ ] Dialog and Conversation Strategies;
- [ ] Environment Interaction (like Brogue[^14], Pokemon[^13] and Metroid[^15]);
- [ ] Weapons Strategy (like Brogue[^12]);
- [ ] Strange and Hostil Environment (like Roadside Picnic[^7] and Annihilation[^8] with elements of Debris[^9] and Doctor Who[^10]).

## History

You are an entity in a world of war and research, training to become a warrior. As part of your training, you must choose a race, a class, and powers. You can test different combinations and then you must join the quest researching new races, classes, powers, and weapons for your world.

## The Game

Initially, the user can choose a race, class, weapons, and powers among the initials. Then, he can choose a world to explore in which he must survive and return with the research data. This enables other races, classes, weapons, or powers which can be used in new explorations. The worlds are procedurally generated with only one exit and different puzzles, traps, and enemies. The powers have different skills progress after the user overcomes the corresponding challenges. The exploration provides adequate challenges based on user choices. The weapons, as the powers, have different skills the correspondent challenges. Besides the powers and skills challenges, the world presents different enemies and bosses. The bosses can be based on race and class generating data for new explorations. Deaths in the exploration generate a boss for the type of death. These bosses reward an additional treasure, but they are based on the state of the player before his death. The objective is to explore the dominating creator world with success. That is the most dangerous and difficult world rewarding power of freedom.


[^1]: [Daemon Powers](https://wiki.daemon.com.br/index.php?title=Supers_RPG)
[^2]: [Diablo 3 - Monters](https://www.reddit.com/r/Diablo/comments/tuhcc/whats_the_difference_between_champion_rare_and/)
[^3]: [Zelda - Puzzles](https://gamerant.com/zelda-hardest-puzzles-how-to-solve/)
[^4]: [Pokemon - Puzzles](https://www.thegamer.com/pokemon-hardest-puzzles-across-all-games-ranked/)
[^5]: [Video Game - Puzzles](https://www.denofgeek.com/games/hardest-video-game-puzzles/)
[^6]: [RPG - Puzzles](https://www.reddit.com/r/rpg/comments/1l72cw/10000_greatest_traps_puzzles/)
[^7]: [Roadside Picnic](https://en.wikipedia.org/wiki/Roadside_Picnic)
[^8]: [Annihilation](https://en.wikipedia.org/wiki/Annihilation_(VanderMeer_novel))
[^9]: [Debris](https://en.wikipedia.org/wiki/Debris_(TV_series))
[^10]: [Doctor Who](https://pt.wikipedia.org/wiki/Doctor_Who)
[^11]: [Solarus](https://www.solarus-games.org/)
[^12]: [Brogue - Weapons](https://brogue.fandom.com/wiki/Category:Weapon)
[^13]: [Pokemon Moves](https://bulbapedia.bulbagarden.net/wiki/Field_move)
[^14]: [Brogue - Terrain](https://brogue.fandom.com/wiki/Terrain_Features)
[^15]: [Metroid Powers](https://metroid.fandom.com/wiki/Super_Metroid#Power-ups_and_suit_upgrades)
[^16]: [Goof Troop - Pluzzes](http://playingwithsuperpower.com/goof-troop-review/)
[^17]: [ANSI Escape Codes](https://gist.github.com/fnky/458719343aabd01cfb17a3a4f7296797)
[^18]: [Rogue Like with Rust](https://bfnightly.bracketproductions.com/chapter_0.html)

[^19]: [Configure Key GIT](https://roelofjanelsinga.com/articles/how-to-setup-gpg-signing-keys-in-github/#:~:text=How%20to%20get%20the%20verified%20flag%20on%20your,use%20your%20GPG%20key%20to%20sign%20commits%20)
[^20]: [triangle rasterization](http://www.sunshine2k.de/coding/java/TriangleRasterization/TriangleRasterization.html)
[^21]: [line rasterization](https://www.javatpoint.com/computer-graphics-bresenhams-line-algorithm)
[^22]: [Field of View](https://www.researchgate.net/publication/347719548_New_Algorithms_for_Computing_Field_of_Vision_over_2D_Grids)
[^23]: [Field of View - Rogue](http://www.roguebasin.com/index.php/Comparative_study_of_field_of_view_algorithms_for_2D_grid_based_worlds)
[^24]: [Game Algorithms](https://www.phstudios.com/game-algorithm-series/)
[^25]: [Brogue](https://sites.google.com/site/broguegame/)
[^26]: [Multiple Body](https://www.gridsagegames.com/blog/2020/04/developing-multitile-creatures-roguelikes/)
[^27]: [IA] (http://www.roguebasin.com/index.php/Roguelike_Intelligence_-_Stateless_AIs)
[^28]: [IA - path](https://news.ycombinator.com/item?id=22848888)
[^29]: [Dijkstra map](http://www.roguebasin.com/index.php?title=The_Incredible_Power_of_Dijkstra_Maps)
[^30]: [Dijkstra map - Rogue Basin](http://www.roguebasin.com/index.php/Dijkstra_Maps_Visualized)

### Dependency

[Avalonia UI](https://avaloniaui.net)

WindowsBase to Sound