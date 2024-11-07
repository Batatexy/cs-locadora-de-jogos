// Seleciona todas as linhas da tabela
document.querySelectorAll('.linha').forEach(row =>
{
    

    


    // Ao passar o mouse sobre a linha
    row.addEventListener('mouseenter', function ()
    {
        const elemento = document.querySelector('.fundo');
        const opacityValue = window.getComputedStyle(elemento).getPropertyValue('opacity');
        console.log("O valor de opacity é:", opacityValue);
        
        const backgroundUrl = this.getAttribute('data-background-url');
        const fundoElement = document.querySelector('.fundo');

        // Define o fundo e torna ele visível

        fundoElement.style.backgroundImage = `url(${backgroundUrl})`;
        fundoElement.classList.add('visible');
    });

    // Ao sair o mouse da linha
    row.addEventListener('mouseleave', function ()
    {
        const fundoElement = document.querySelector('.fundo');

        // Remove a imagem de fundo e a visibilidade
        fundoElement.classList.remove('visible');
    });
});