using SixLabors.ImageSharp.Formats.Bmp;
using ImageSharpImage = SixLabors.ImageSharp.Image;

namespace visualizadorTGA
{
    public partial class Form1 : Form
    {
        private const float ZoomPasso = 1.10f;
        private const float ZoomMinimo = 0.05f;
        private const float ZoomMaximo = 20f;
        private static readonly string[] ExtensoesNavegaveis =
        {
            ".tga", ".png", ".jpg", ".jpeg", ".bmp", ".gif", ".tif", ".tiff", ".webp"
        };

        private float zoomAtual = 1f;
        private List<string> imagensDaPasta = new();
        private int indiceImagemAtual = -1;
        private bool centralizarImagemAtiva;
        private ContextMenuStrip? menuContextoImagem;
        private ToolStripMenuItem? menuContextoZoom100;
        private ToolStripMenuItem? menuContextoRotacionar;
        private ToolStripMenuItem? menuContextoCantoSuperiorEsquerdo;
        private ToolStripMenuItem? menuContextoCentralizar;

        public Form1()
        {
            InitializeComponent();
            KeyPreview = true;

            pictureBoxImagem.MouseWheel += areaImagem_MouseWheel;
            panelImagem.MouseWheel += areaImagem_MouseWheel;
            pictureBoxImagem.MouseEnter += areaImagem_MouseEnter;
            panelImagem.MouseEnter += areaImagem_MouseEnter;

            ConfigurarMenuContextoImagem();
            AtualizarIndicadorZoom();
            labelZoom.BringToFront();
        }

        private void abrirTgaToolStripMenuItem_Click(object? sender, EventArgs e)
        {
            using var dialog = new OpenFileDialog
            {
                Title = "Abrir imagem TGA",
                Filter = "Imagens TGA (*.tga)|*.tga|Todos os arquivos (*.*)|*.*",
                Multiselect = false,
                CheckFileExists = true
            };

            if (dialog.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            CarregarImagemTga(dialog.FileName);
        }

        private void sairToolStripMenuItem_Click(object? sender, EventArgs e)
        {
            Close();
        }

        private void CarregarImagemTga(string caminhoArquivo)
        {
            try
            {
                using ImageSharpImage imagem = ImageSharpImage.Load(caminhoArquivo);
                using var stream = new MemoryStream();
                imagem.Save(stream, new BmpEncoder());
                stream.Position = 0;

                using var bitmapTemp = new Bitmap(stream);
                var bitmapFinal = new Bitmap(bitmapTemp);

                AtualizarImagem(bitmapFinal);
                AtualizarListaNavegacao(caminhoArquivo);
                Text = $"Visualizador TGA - {Path.GetFileName(caminhoArquivo)}";
            }
            catch (SixLabors.ImageSharp.UnknownImageFormatException)
            {
                MessageBox.Show(
                    this,
                    "Nao foi possivel reconhecer este arquivo como imagem valida.",
                    "Formato invalido",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    this,
                    $"Erro ao abrir a imagem:\n{ex.Message}",
                    "Falha ao abrir arquivo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void AtualizarImagem(Bitmap novaImagem)
        {
            var imagemAnterior = pictureBoxImagem.Image;
            pictureBoxImagem.Image = novaImagem;
            imagemAnterior?.Dispose();

            zoomAtual = CalcularZoomInicial(novaImagem);
            RedimensionarImagemExibida(resetarScroll: true);
            AtualizarIndicadorZoom();
        }

        private float CalcularZoomInicial(System.Drawing.Image imagem)
        {
            if (panelImagem.ClientSize.Width <= 0 || panelImagem.ClientSize.Height <= 0)
            {
                return 1f;
            }

            float zoomHorizontal = panelImagem.ClientSize.Width / (float)imagem.Width;
            float zoomVertical = panelImagem.ClientSize.Height / (float)imagem.Height;
            float zoomParaCaber = Math.Min(zoomHorizontal, zoomVertical);

            return Math.Clamp(Math.Min(1f, zoomParaCaber), ZoomMinimo, ZoomMaximo);
        }

        private void areaImagem_MouseEnter(object? sender, EventArgs e)
        {
            pictureBoxImagem.Focus();
        }

        private void areaImagem_MouseWheel(object? sender, MouseEventArgs e)
        {
            if (pictureBoxImagem.Image is null)
            {
                return;
            }

            Point pontoPainel = panelImagem.PointToClient(Cursor.Position);
            if (!panelImagem.ClientRectangle.Contains(pontoPainel))
            {
                return;
            }

            float passos = e.Delta / 120f;
            if (passos == 0f)
            {
                return;
            }

            float fatorZoom = (float)Math.Pow(ZoomPasso, passos);
            AjustarZoom(zoomAtual * fatorZoom, pontoPainel);
        }

        private void AjustarZoom(float novoZoom, Point pontoAncoragemPainel)
        {
            if (pictureBoxImagem.Image is null)
            {
                return;
            }

            novoZoom = Math.Clamp(novoZoom, ZoomMinimo, ZoomMaximo);
            if (Math.Abs(novoZoom - zoomAtual) < 0.0001f)
            {
                return;
            }

            int deslocamentoAtualX = panelImagem.AutoScroll ? -panelImagem.AutoScrollPosition.X : 0;
            int deslocamentoAtualY = panelImagem.AutoScroll ? -panelImagem.AutoScrollPosition.Y : 0;

            float pontoImagemX = (pontoAncoragemPainel.X + deslocamentoAtualX - pictureBoxImagem.Left) / zoomAtual;
            float pontoImagemY = (pontoAncoragemPainel.Y + deslocamentoAtualY - pictureBoxImagem.Top) / zoomAtual;

            zoomAtual = novoZoom;
            RedimensionarImagemExibida();
            AtualizarIndicadorZoom();

            if (!panelImagem.AutoScroll)
            {
                return;
            }

            int novoDeslocamentoX = (int)Math.Round((pontoImagemX * zoomAtual) + pictureBoxImagem.Left - pontoAncoragemPainel.X);
            int novoDeslocamentoY = (int)Math.Round((pontoImagemY * zoomAtual) + pictureBoxImagem.Top - pontoAncoragemPainel.Y);
            panelImagem.AutoScrollPosition = new Point(Math.Max(0, novoDeslocamentoX), Math.Max(0, novoDeslocamentoY));
        }

        private void RedimensionarImagemExibida(bool resetarScroll = false)
        {
            if (pictureBoxImagem.Image is null)
            {
                return;
            }

            int novaLargura = Math.Max(1, (int)Math.Round(pictureBoxImagem.Image.Width * zoomAtual));
            int novaAltura = Math.Max(1, (int)Math.Round(pictureBoxImagem.Image.Height * zoomAtual));

            pictureBoxImagem.Size = new Size(novaLargura, novaAltura);
            PosicionarImagemNoPainel(resetarScroll);
        }

        private void PosicionarImagemNoPainel(bool resetarScroll)
        {
            if (pictureBoxImagem.Image is null)
            {
                return;
            }

            bool cabeHorizontal = pictureBoxImagem.Width <= panelImagem.ClientSize.Width;
            bool cabeVertical = pictureBoxImagem.Height <= panelImagem.ClientSize.Height;
            bool deveCentralizar = centralizarImagemAtiva && cabeHorizontal && cabeVertical;

            if (deveCentralizar)
            {
                panelImagem.AutoScroll = false;
                int posicaoX = (panelImagem.ClientSize.Width - pictureBoxImagem.Width) / 2;
                int posicaoY = (panelImagem.ClientSize.Height - pictureBoxImagem.Height) / 2;
                pictureBoxImagem.Location = new Point(Math.Max(0, posicaoX), Math.Max(0, posicaoY));
                return;
            }

            bool estavaSemScroll = !panelImagem.AutoScroll;
            panelImagem.AutoScroll = true;
            pictureBoxImagem.Location = Point.Empty;
            panelImagem.AutoScrollMinSize = pictureBoxImagem.Size;

            if (resetarScroll || estavaSemScroll)
            {
                if (centralizarImagemAtiva)
                {
                    int centroX = Math.Max(0, (pictureBoxImagem.Width - panelImagem.ClientSize.Width) / 2);
                    int centroY = Math.Max(0, (pictureBoxImagem.Height - panelImagem.ClientSize.Height) / 2);
                    panelImagem.AutoScrollPosition = new Point(centroX, centroY);
                }
                else
                {
                    panelImagem.AutoScrollPosition = new Point(0, 0);
                }
            }
        }

        private void AtualizarIndicadorZoom()
        {
            labelZoom.Text = $"{zoomAtual * 100f:0}%";
        }

        private void ConfigurarMenuContextoImagem()
        {
            menuContextoImagem = new ContextMenuStrip(components);

            menuContextoZoom100 = new ToolStripMenuItem("Zoom 100%");
            menuContextoZoom100.Click += (_, _) => AplicarZoom100();

            menuContextoRotacionar = new ToolStripMenuItem("Rotacionar");
            var menuContextoGirarDireita = new ToolStripMenuItem("90 graus horario");
            menuContextoGirarDireita.Click += (_, _) => RotacionarImagem(RotateFlipType.Rotate90FlipNone);
            var menuContextoGirarEsquerda = new ToolStripMenuItem("90 graus anti-horario");
            menuContextoGirarEsquerda.Click += (_, _) => RotacionarImagem(RotateFlipType.Rotate270FlipNone);
            menuContextoRotacionar.DropDownItems.AddRange(
                new ToolStripItem[] { menuContextoGirarDireita, menuContextoGirarEsquerda });

            menuContextoCantoSuperiorEsquerdo = new ToolStripMenuItem("Canto superior esquerdo");
            menuContextoCantoSuperiorEsquerdo.Click += (_, _) => DefinirAlinhamentoCantoSuperiorEsquerdo();

            menuContextoCentralizar = new ToolStripMenuItem("Centralizar no programa");
            menuContextoCentralizar.Click += (_, _) => DefinirAlinhamentoCentralizado();

            menuContextoImagem.Items.AddRange(
                new ToolStripItem[]
                {
                    menuContextoZoom100,
                    menuContextoRotacionar,
                    new ToolStripSeparator(),
                    menuContextoCantoSuperiorEsquerdo,
                    menuContextoCentralizar
                });

            menuContextoImagem.Opening += menuContextoImagem_Opening;
            pictureBoxImagem.ContextMenuStrip = menuContextoImagem;
            panelImagem.ContextMenuStrip = menuContextoImagem;
        }

        private void menuContextoImagem_Opening(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            bool possuiImagem = pictureBoxImagem.Image is not null;

            if (menuContextoZoom100 is not null)
            {
                menuContextoZoom100.Enabled = possuiImagem;
            }

            if (menuContextoRotacionar is not null)
            {
                menuContextoRotacionar.Enabled = possuiImagem;
            }

            if (menuContextoCantoSuperiorEsquerdo is not null)
            {
                menuContextoCantoSuperiorEsquerdo.Enabled = possuiImagem;
                menuContextoCantoSuperiorEsquerdo.Checked = possuiImagem && !centralizarImagemAtiva;
            }

            if (menuContextoCentralizar is not null)
            {
                menuContextoCentralizar.Enabled = possuiImagem;
                menuContextoCentralizar.Checked = possuiImagem && centralizarImagemAtiva;
            }
        }

        private void AplicarZoom100()
        {
            if (pictureBoxImagem.Image is null)
            {
                return;
            }

            zoomAtual = 1f;
            RedimensionarImagemExibida(resetarScroll: true);
            AtualizarIndicadorZoom();
        }

        private void RotacionarImagem(RotateFlipType tipoRotacao)
        {
            if (pictureBoxImagem.Image is null)
            {
                return;
            }

            float zoomAnterior = zoomAtual;

            using var bitmapTemporario = new Bitmap(pictureBoxImagem.Image);
            bitmapTemporario.RotateFlip(tipoRotacao);
            var bitmapRotacionado = new Bitmap(bitmapTemporario);

            var imagemAnterior = pictureBoxImagem.Image;
            pictureBoxImagem.Image = bitmapRotacionado;
            imagemAnterior?.Dispose();

            zoomAtual = Math.Clamp(zoomAnterior, ZoomMinimo, ZoomMaximo);
            RedimensionarImagemExibida(resetarScroll: true);
            AtualizarIndicadorZoom();
        }

        private void DefinirAlinhamentoCantoSuperiorEsquerdo()
        {
            if (centralizarImagemToolStripMenuItem.Checked)
            {
                centralizarImagemToolStripMenuItem.Checked = false;
                return;
            }

            centralizarImagemAtiva = false;
            PosicionarImagemNoPainel(resetarScroll: true);
        }

        private void DefinirAlinhamentoCentralizado()
        {
            if (!centralizarImagemToolStripMenuItem.Checked)
            {
                centralizarImagemToolStripMenuItem.Checked = true;
                return;
            }

            centralizarImagemAtiva = true;
            PosicionarImagemNoPainel(resetarScroll: true);
        }

        private void centralizarImagemToolStripMenuItem_CheckedChanged(object? sender, EventArgs e)
        {
            centralizarImagemAtiva = centralizarImagemToolStripMenuItem.Checked;
            PosicionarImagemNoPainel(resetarScroll: true);
        }

        private void panelImagem_Resize(object? sender, EventArgs e)
        {
            PosicionarImagemNoPainel(resetarScroll: false);
        }

        private void AtualizarListaNavegacao(string caminhoArquivoAtual)
        {
            string? pasta = Path.GetDirectoryName(caminhoArquivoAtual);
            if (string.IsNullOrWhiteSpace(pasta) || !Directory.Exists(pasta))
            {
                imagensDaPasta.Clear();
                indiceImagemAtual = -1;
                return;
            }

            imagensDaPasta = Directory
                .GetFiles(pasta)
                .Where(EhArquivoNavegavel)
                .OrderBy(Path.GetFileName, StringComparer.CurrentCultureIgnoreCase)
                .ToList();

            if (!imagensDaPasta.Contains(caminhoArquivoAtual, StringComparer.OrdinalIgnoreCase))
            {
                imagensDaPasta.Add(caminhoArquivoAtual);
                imagensDaPasta = imagensDaPasta
                    .OrderBy(Path.GetFileName, StringComparer.CurrentCultureIgnoreCase)
                    .ToList();
            }

            indiceImagemAtual = imagensDaPasta.FindIndex(caminho =>
                string.Equals(caminho, caminhoArquivoAtual, StringComparison.OrdinalIgnoreCase));
        }

        private static bool EhArquivoNavegavel(string caminhoArquivo)
        {
            string extensao = Path.GetExtension(caminhoArquivo);
            return ExtensoesNavegaveis.Contains(extensao, StringComparer.OrdinalIgnoreCase);
        }

        private void NavegarImagem(int deslocamento)
        {
            if (indiceImagemAtual < 0 || imagensDaPasta.Count == 0)
            {
                return;
            }

            int novoIndice = indiceImagemAtual + deslocamento;
            if (novoIndice < 0 || novoIndice >= imagensDaPasta.Count)
            {
                return;
            }

            CarregarImagemTga(imagensDaPasta[novoIndice]);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Left)
            {
                NavegarImagem(-1);
                return true;
            }

            if (keyData == Keys.Right)
            {
                NavegarImagem(1);
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            pictureBoxImagem.Image?.Dispose();
            base.OnFormClosed(e);
        }
    }
}
