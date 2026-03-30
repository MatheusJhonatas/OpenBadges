import { useEffect, useState } from "react";
import { useParams, useNavigate } from "react-router-dom";
import { QRCodeSVG } from "qrcode.react";
import { Button } from "../components/ui/Button";
import {ArrowLeft, Copy} from "lucide-react";
import {FaLinkedin, FaWhatsapp,FaTwitter} from "react-icons/fa";

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
    return url.startsWith("http")
      ? url
      : `${window.location.origin}${url}`;
  };

  // ===== SHARE =====
  const open = (url: string) =>
    window.open(url, "_blank", "noopener,noreferrer");

  const shareLinkedIn = () => {
    if (!shareUrl) return;
    open(
      `https://www.linkedin.com/sharing/share-offsite/?url=${encodeURIComponent(shareUrl)}`
    );
  };

  const shareTwitter = () => {
    if (!shareUrl) return;
    open(
      `https://twitter.com/intent/tweet?url=${encodeURIComponent(shareUrl)}`
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
            <p className="text-gray-700 mb-4">
              {badge.description}
            </p>

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

              <div className="flex flex-wrap gap-6">
                <Button onClick={shareLinkedIn}>
                    <FaLinkedin size={18}/>
                    LinkedIn</Button>
                <Button onClick={shareTwitter}>
                    <FaTwitter size={16}/>
                    Twitter</Button>
                <Button onClick={shareWhatsApp}>
                    <FaWhatsapp size={16}/>
                    WhatsApp</Button>
                <Button onClick={copyLink}>
                    <Copy size={16}/>
                    Copiar Link</Button>
              </div>
            </div>

          </div>
        ) : (
          <p className="text-gray-500">Carregando badge...</p>
        )}
      </div>
    </div>
  );
};