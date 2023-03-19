# Simple Game Engine

Arquitetura baseada no padrão Component para escalabilidade horizontal baseada em composição. A classe Game é a principal que deve ser instanciada com um subsistema de renderização. Após isso, o jogo deve ser iniciado pelo método Start, responsável por carregar e instanciar os dados do jogo, e depois executado pelo método Run, que inicia o laço principal do jogo até que o método Stop seja invocado.

O jogo é composto por diferentes entidades e cada uma tendo múltiplos componentes. Cada componente deve ser responsável por uma funcionalidade específica da entidade, por exemplo, um para manipulação de entrada, outro para renderização, outro para sons e efeitos, e assim por diante. O objetivo é que a lógica de cada componente seja independente dos demais.

Desse modo, cada componente pode estar associado a um ou mais sistemas representados pela classe SubSystem. Cada sistema é responsável por coordenar os componentes registrados, por exemplo, o sistema de física deve coordenar o movimento e colisão dos componentes. Assim, o jogo será composto por múltiplos sistemas responsáveis por características específicas dos jogo como física, inteligência artificial, música, entre outros.

As classes SubSystem e Component devem ser especializadas dando origem a novos sistemas aumentando as capacidades da máquina do jogo. Atualmente, somente o sistema de entrada de dados através do console está implementado. O trecho de código abaixo exemplifica o uso do arquitetura:

```c#
var console = new ConsoleInputSystem(); // sistema de entrada de dados do console
var render = new SubSystem(); // vazio

var game = new Game(render); // novo jogo com um sistema de renderização
game.AttachSystem(console); // adição do sistema de entrada de dados

var entity = game.CreateEntity();
var echo = new EchoComponent(); // componente especializado
entity.AttachComponent(echo); // adição do componente
console.Register(echo); // registro do componente no sistema

game.Start(); // carregamento de dados
game.Run(); // execução


// especialização do componente
class EchoComponent : ConsoleInputComponent {

    public override bool OnKeyDown(char c)
    {
        Console.WriteLine("Key Down " + c);
        return true; // evento processado, a entrada não será apresentada para outro componente
    }

    public override bool OnKeyPressed(char c)
    {
        Console.WriteLine("Key Pressed " + c);
        return true;
    }

    public override bool OnKeyUp(char c)
    {
        Console.WriteLine("Key Up " + c);
        return true;
    }
}

```
