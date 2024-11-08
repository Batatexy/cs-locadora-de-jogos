var itemClickado;

var imageTrigger = 0;
const imageTimerValue = 50;
var imageTimer = imageTimerValue;

// Set itemClickado
function atualizarItemClickado(novoItemClickado)
{
    itemClickado = novoItemClickado;
}

// Get itemClickado
function retornarItemClickado()
{
    return itemClickado;
}

// Função para redirecionar às páginas de editar, detalhes ou deletar
function redirecionarPagina(tabela, funcao)
{
    const id = retornarItemClickado();

    // Redireciona para a página de edição com o ID obtido
    if (id != null)
    {
        window.location.href = `/${tabela}/${funcao}/${id}`;
    }
}



// Seleciona todas as linhas da tabela
document.querySelectorAll('.linha-jogos').forEach(row =>
{
    // Ao passar o mouse sobre a linha
    row.addEventListener('mouseenter', function ()
    {     
        const backgroundUrl = this.getAttribute('data-background-url');
        const fundoElement = document.querySelector('.fundo');

        console.log("imageTimer", imageTimer);


        // Define o fundo e torna ele visível
        // Ainda com problemas, desenvolver um timer para não ocorrer o bug de passar o mouse por diversos jogos e o fundo ficar piscando
        // Se der pra fazer um sistema onde só muda o wallpaper quando o mesmo estiver carregado, perfeito
        if (imageTimer > 0)
        {
            fundoElement.style.backgroundImage = `url(${backgroundUrl})`;
            fundoElement.classList.add('visible');
        }
    });

    // Ao sair o mouse da linha
    row.addEventListener('mouseleave', function ()
    {
        imageTimer = imageTimerValue

        const fundoElement = document.querySelector('.fundo');

        // Remove a imagem de fundo e a visibilidade
        fundoElement.classList.remove('visible');
    });
});