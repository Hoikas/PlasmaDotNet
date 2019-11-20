using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Plasma;

namespace VaultMangler.Controls {
    public partial class ImageNodeControl : NodeBackedControl {

        public override pnVaultNode VaultNode {
            set {
                pnVaultImageNode img = new pnVaultImageNode(value);
                if (img.ImageType != pnVaultImageNode.ImgType.kNone)
                    fKiImage.Image = Bitmap.FromStream(new MemoryStream(img.ImageData));
                fIsPng.Checked = img.ImageType == pnVaultImageNode.ImgType.kPNG;
                fDescription.Text = img.ImageName;
            }
        }

        public ImageNodeControl() {
            InitializeComponent();
        }

        public override void SaveDamage(pnVaultNode node) {
            pnVaultImageNode img = new pnVaultImageNode(node);
            img.ImageName = fDescription.Text;
            if (fIsPng.Checked)
                img.ImageType = pnVaultImageNode.ImgType.kPNG;
            else
                img.ImageType = pnVaultImageNode.ImgType.kJPEG;

            MemoryStream ms = new MemoryStream();
            if (fIsPng.Checked)
                fKiImage.Image.Save(ms, ImageFormat.Png);
            else
                fKiImage.Image.Save(ms, ImageFormat.Jpeg);
            img.ImageData = ms.ToArray();
            ms.Close();
        }

        private void IExportImage(object sender, LinkLabelLinkClickedEventArgs e) {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.AddExtension = true;

            if (fIsPng.Checked) {
                sfd.DefaultExt = ".png";
                sfd.Filter = "Portable Network Graphics (*.png)|*.png";
            } else {
                sfd.DefaultExt = ".jpg";
                sfd.Filter = "JPEG Image (*.jpg, *.jpeg)|*.jpg;*.jpeg";
            }

            if (sfd.ShowDialog(this) == DialogResult.OK) {
                ImageFormat fmt = fIsPng.Checked ? ImageFormat.Jpeg : ImageFormat.Png;
                fKiImage.Image.Save(sfd.FileName, fmt);
            }
        }

        private void IReplaceImage(object sender, LinkLabelLinkClickedEventArgs e) {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.CheckFileExists = true;
            ofd.Filter = "JPEG Image (*.jpg, *.jpeg)|*.jpg;*.jpeg|Portable Network Graphics (*.png)|*.png";
            ofd.Title = "Select Image";
            if (ofd.ShowDialog(this) == DialogResult.OK) {
                fKiImage.Image = Bitmap.FromStream(ofd.OpenFile());
                fIsPng.Checked = ofd.FileName.EndsWith("png");
            }
        }
    }
}
