import { useEffect, useState } from "react";
import { useParams, useNavigate } from "react-router-dom";
import { QRCodeSVG } from "qrcode.react";
import { Button } from "../components/ui/Button";
import { ArrowLeft, Copy, FileJson } from "lucide-react";
import html2canvas from "html2canvas";
import {
  FaLinkedin,
  FaWhatsapp,
  FaTwitter,
  FaDownload,
  FaQrcode,
  FaPager,
} from "react-icons/fa";

type BadgeDetails = {
  id: string;
  name: string;
  createdAt: string;
  description: string;
  slug: string;
  image?: {
    url: string;
  };
  criteria?: {
    narrative: string;
  };
};

export const BadgeDetailsPage = () => {
  const { id } = useParams();
  const navigate = useNavigate();

  const [badge, setBadge] = useState<BadgeDetails | null>(null);

  const shareUrl = badge?.id
    ? `${window.location.origin}/badge/${badge.id}`
    : "";

  useEffect(() => {
    if (!id) return;

    fetch(`http://localhost:5045/api/badges/${id}`)
      .then((res) => res.json())
      .then(setBadge)
      .catch(console.error);
  }, [id]);

  const formatDate = (date: string) =>
    new Date(date).toLocaleDateString("pt-BR", {
      day: "2-digit",
      month: "long",
      year: "numeric",
    });

  const getImageUrl = (url?: string) => {
    if (!url) return "";
    return url.startsWith("http") ? url : `${window.location.origin}${url}`;
  };

  // ===== SHARE =====
  const open = (url: string) =>
    window.open(url, "_blank", "noopener,noreferrer");

  const shareLinkedIn = () => {
    if (!shareUrl) return;
    open(
      `https://www.linkedin.com/sharing/share-offsite/?url=${encodeURIComponent(shareUrl)}`,
    );
  };

  const shareTwitter = () => {
    if (!shareUrl) return;
    open(
      `https://twitter.com/intent/tweet?url=${encodeURIComponent(shareUrl)}`,
    );
  };

  const shareWhatsApp = () => {
    if (!shareUrl) return;
    open(`https://wa.me/?text=${encodeURIComponent(shareUrl)}`);
  };

  const copyLink = async () => {
    if (!shareUrl) return;
    await navigator.clipboard.writeText(shareUrl);
    alert("Link copiado!");
  };

  // ===== DOWNLOAD BADGE =====
  const downloadBadgeImage = async () => {
    try {
      const element = document.getElementById("badge-capture");
      if (!element) return;

      const canvas = await html2canvas(element, {
        scale: 2,
        useCORS: true,
      });

      const link = document.createElement("a");
      link.download = `${badge?.slug || "badge"}.png`;
      link.href = canvas.toDataURL("image/png");

      document.body.appendChild(link);
      link.click();
      link.remove();
    } catch (error) {
      console.error("Erro ao gerar imagem:", error);
    }
  };

  return (
    <div className="p-8 flex justify-center">
      <div className="w-full max-w-2xl">
        {/* VOLTAR */}
        <button
          onClick={() => navigate("/meus-badges")}
          className="flex items-center gap-2 bg-blue-600 text-white px-4 py-2 rounded-lg text-sm hover:bg-blue-700 transition-colors mb-4"
        >
          <ArrowLeft size={16} />
          Voltar para Meus Badges
        </button>

        {/* CARD */}
        {badge ? (
          <div className="bg-white border rounded-xl shadow-sm p-6">
            {/* HEADER */}
            <div className="flex justify-between items-start mb-4">
              <div>
                <h2 className="text-xl font-semibold">{badge.name}</h2>
                <p className="text-sm text-gray-500">
                  Emitido pelo Núcleo de Formação - NTT DATA
                </p>
              </div>

              <span className="bg-blue-100 text-blue-700 text-xs px-3 py-1 rounded-full">
                Válido
              </span>
            </div>

            {/* IMAGEM */}
            <div className="flex justify-center my-6">
              {badge.image?.url && (
                <img
                  src={getImageUrl(badge.image.url)}
                  alt={badge.name}
                  className="w-48"
                />
              )}
            </div>

            {/* DATA */}
            <div className="border-t border-gray-300 pt-4 mb-4 text-sm text-gray-500">
              Emitido em {formatDate(badge.createdAt)}
            </div>

            {/* DESCRIÇÃO */}
            <p className="text-gray-700 mb-4">{badge.description}</p>

            {/* CRITÉRIOS */}
            {badge.criteria && (
              <p className="text-sm text-gray-600 mt-2">
                <span className="font-semibold">Critérios:</span>{" "}
                {badge.criteria.narrative}
              </p>
            )}

            {/* QR */}
            <div className="border-t border-gray-300 pt-4 mt-6">
              <h3 className="text-sm font-medium mb-3">
                Código QR para Verificação
              </h3>

              <div className="flex justify-center">
                <QRCodeSVG value={shareUrl} size={128} />
              </div>
            </div>

            {/* COMPARTILHAR */}
            <div className="border-t border-gray-300 pt-4 mt-6">
              <h3 className="text-sm font-medium mb-3">
                Compartilhar Credencial
              </h3>

              <p className="text-sm text-gray-600 mt-2">
                Compartilhe esta credencial com outras pessoas ou em suas redes
                profissionais. Utilize os botões abaixo para divulgar sua
                conquista de forma rápida e confiável.
              </p>

              <div className="flex flex-wrap gap-6 justify-center mt-4">
                <Button onClick={shareLinkedIn}>
                  <FaLinkedin size={18} />
                  LinkedIn
                </Button>
                <Button onClick={shareTwitter}>
                  <FaTwitter size={18} />
                  Twitter
                </Button>
                <Button onClick={shareWhatsApp}>
                  <FaWhatsapp size={18} />
                  WhatsApp
                </Button>
                <Button onClick={copyLink}>
                  <Copy size={18} />
                  Copiar Link
                </Button>
              </div>

              {/* AÇÕES */}
              <div className="mt-8">
                <h3 className="text-sm font-medium mb-3">Ações</h3>

                <p className="text-sm text-gray-600 mt-2">
                  Gerencie e utilize sua credencial conforme necessário. Aqui
                  você pode baixar arquivos, validar informações ou acessar os
                  dados técnicos da emissão.
                </p>

                <div className="flex flex-wrap gap-5 mt-4 justify-center">
                  <Button onClick={downloadBadgeImage}>
                    <FaDownload size={18} />
                    Baixar Badge
                  </Button>
                  <Button>
                    <FaQrcode size={18} />
                    Baixar QR Code
                  </Button>
                  <Button>
                    <FaPager size={18} />
                    Página de Verificação
                  </Button>
                  <Button>
                    <FileJson size={18} />
                    JSON da Assertion
                  </Button>
                </div>
              </div>
            </div>
          </div>
        ) : (
          <p className="text-gray-500">Carregando badge...</p>
        )}

        {/* CAPTURE */}
        {badge && (
          <div
            id="badge-capture"
            className="fixed -z-10 top-0 left-0"
            style={{
              width: "900px",
              padding: "50px",
              background: "linear-gradient(135deg, #0f172a, #1e40af)",
              color: "#ffffff",
              fontFamily: "Arial, sans-serif",
              borderRadius: "20px",
            }}
          >
            {/* HEADER */}
            <div style={{ textAlign: "center", marginBottom: "30px" }}>
              <h1 style={{ fontSize: "32px", fontWeight: "bold" }}>
                Certificado de Conquista
              </h1>
              <p style={{ fontSize: "14px", opacity: 0.8 }}>
                Núcleo de Formação - NTT DATA
              </p>
            </div>

            {/* TEXTO FORMAL */}
            <p
              style={{
                textAlign: "center",
                fontSize: "16px",
                marginBottom: "10px",
              }}
            >
              Certificamos que
            </p>

            {/* NOME DO USUÁRIO (placeholder) */}
            <h2
              style={{
                textAlign: "center",
                fontSize: "26px",
                fontWeight: "bold",
                marginBottom: "20px",
              }}
            >
              Matheus Jhonatas
            </h2>

            {/* BADGE */}
            <div
              style={{
                display: "flex",
                justifyContent: "center",
                marginBottom: "20px",
              }}
            >
              {badge.image?.url && (
                <img
                  src={getImageUrl(badge.image.url)}
                  crossOrigin="anonymous"
                  style={{
                    width: "150px",
                    background: "#ffffff",
                    padding: "12px",
                    borderRadius: "14px",
                  }}
                />
              )}
            </div>

            {/* TÍTULO PRINCIPAL */}
            <h3
              style={{
                textAlign: "center",
                fontSize: "20px",
                fontWeight: "bold",
                marginBottom: "10px",
              }}
            >
              {badge.name}
            </h3>

            {/* DESCRIÇÃO */}
            <p
              style={{
                textAlign: "center",
                fontSize: "14px",
                maxWidth: "650px",
                margin: "0 auto 16px",
                opacity: 0.9,
              }}
            >
              {badge.description}
            </p>

            {/* DATA */}
            <p
              style={{
                textAlign: "center",
                fontSize: "12px",
                opacity: 0.7,
              }}
            >
              Emitido em {formatDate(badge.createdAt)}
            </p>

            {/* DIVIDER */}
            <div
              style={{
                height: "1px",
                background: "rgba(255,255,255,0.2)",
                margin: "30px 0",
              }}
            />

            {/* FOOTER */}
            <div
              style={{
                display: "flex",
                justifyContent: "space-between",
                alignItems: "center",
              }}
            >
              {/* ESQUERDA */}
              <div>
                <p style={{ fontSize: "12px", opacity: 0.7 }}>
                  Verificação digital
                </p>
                <p style={{ fontSize: "14px", fontWeight: "bold" }}>
                  openbadges.local
                </p>
              </div>

              {/* DIREITA (QR + label) */}
              <div style={{ textAlign: "center" }}>
                <QRCodeSVG value={shareUrl} size={100} />
                <p style={{ fontSize: "10px", marginTop: "6px", opacity: 0.6 }}>
                  Escaneie para validar
                </p>
              </div>

            </div>
          </div>
        )}
      </div>
    </div>
  );
};
