
# Simple Game Engine

### Todo

- [ ] Fixar dimensão mínima 640x360.
- [ ] Tela inicial com opções.
- [ ] Tela de carregamento. Deve ser lançada no início de aplicação exibindo uma barra de progresso enquanto os recursos são carregados.
- [ ] Sistema de interface de usuário.
- [ ] Redimensionamento da tela de desenho.
- [ ] Atualização do sistema sonoro para habilitar a reprodução em diferentes volumes.

### In Progress

### Testing

### Done

- [x] Plataforma abstraindo detalhes de carregamento de imagens, som, janela gráfica, entre outros.
- [x] Sistema para posicionamento discreto.
- [x] Sistema para desenho de sprites e animações.
- [x] Sistema para movimentações baseado nas movimentações discretas.
- [x] SIstema de câmera efetuando o recorte da área de desenho.

### Comments

As dimensões padrões para os dispositivos são listadas abaixo:
- Monitor displays ranging from 1024×768 to 1920×1080,
- Smartphone displays ranging from 360×640 to 414×896,
- Tablet displays ranging from 601×962 to 1280×800.
Vamos trabalhar com dimensões de 1280x720 e redimensionar para as dimensões mínimas e máxima para smartphones e tablets satisfazendo a razão 16:9.

Tiles de 32x32
    Monitor (Normal): 1280x720  -> 40x22.5
    Smartphone: 640x360         -> 20x11
    Tablet: 1280x720








