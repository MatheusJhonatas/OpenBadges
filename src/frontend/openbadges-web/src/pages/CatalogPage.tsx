import { useEffect, useState, useRef } from "react";
import { BadgeCard } from "../components/ui/BadgeCard";
import { getBadges } from "../services/badgeService";
import { X } from "lucide-react";
import type { Badge } from "../services/badgeService";

export const CatalogPage = () => {
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [badges, setBadges] = useState<Badge[]>([]);
  const [loading, setLoading] = useState(true);
  const [isCreating, setIsCreating] = useState(false);

  const [form, setForm] = useState({
    name: "",
    imageUrl: "",
    description: "",
    criteriaNarrative: "",
  });

  const [errors, setErrors] = useState({
    name: "",
    imageUrl: "",
    description: "",
    criteriaNarrative: "",
  });

  const openButtonRef = useRef<HTMLButtonElement>(null);
  const modalTitleRef = useRef<HTMLHeadingElement>(null);

  // carregar badges
  useEffect(() => {
    getBadges()
      .then((data) =>
        setBadges(
          data.sort(
            (a, b) =>
              new Date(b.createdAt).getTime() -
              new Date(a.createdAt).getTime()
          )
        )
      )
      .catch((error) => console.error("Erro ao buscar badges:", error))
      .finally(() => setLoading(false));
  }, []);

  // foco ao abrir modal
  useEffect(() => {
    if (isModalOpen) {
      setTimeout(() => {
        modalTitleRef.current?.focus();
      }, 100);
    }
  }, [isModalOpen]);

  return (
    <div className="p-8 max-w-6xl mx-auto">
      {/* HEADER */}
      <div className="flex justify-between items-center mb-6">
        <div>
          <h1 className="text-2xl font-bold">Catálogo de Badges</h1>
          <p className="text-gray-600">
            Gerencie os templates de credenciais disponíveis
          </p>
        </div>

        <button
          ref={openButtonRef}
          onClick={() => setIsModalOpen(true)}
          className="bg-blue-600 text-white px-4 py-2 rounded"
        >
          + Novo Badge
        </button>
      </div>

      {loading && <p>Carregando badges...</p>}
      {!loading && badges.length === 0 && <p>Nenhum badge encontrado</p>}

      {!loading && badges.length > 0 && (
        <div className="grid grid-cols-3 gap-6 items-stretch">
          {badges.map((badge) => (
            <BadgeCard
              key={badge.id}
              id={badge.id}
              name={badge.name}
              slug={badge.slug}
              description={badge.description}
              imageUrl={badge.imageUrl}
              criteria={badge.criteria}
            />
          ))}
        </div>
      )}

      {/* MODAL */}
      {isModalOpen && (
        <div className="fixed inset-0 bg-black/50 flex items-center justify-center">
          <div className="bg-white p-6 rounded-lg w-96">
            <div className="flex justify-between items-center mb-4">
              <h2
                ref={modalTitleRef}
                tabIndex={-1}
                className="text-lg font-bold"
              >
                Novo Badge
              </h2>

              <button
                onClick={() => {
                  setIsModalOpen(false);
                  openButtonRef.current?.focus();
                }}
                className="text-gray-500 hover:text-black"
                aria-label="Fechar"
              >
                <X size={20} />
              </button>
            </div>

            <form
              className="space-y-3"
              onSubmit={async (e) => {
                e.preventDefault();

                const newErrors = {
                  name: !form.name.trim()
                    ? "Informe o nome do badge"
                    : "",
                  imageUrl: !form.imageUrl.trim()
                    ? "Informe a URL da imagem"
                    : "",
                  description: !form.description.trim()
                    ? "Informe a descrição"
                    : "",
                  criteriaNarrative: !form.criteriaNarrative.trim()
                    ? "Informe os critérios"
                    : "",
                };

                setErrors(newErrors);

                if (
                  newErrors.name ||
                  newErrors.imageUrl ||
                  newErrors.description ||
                  newErrors.criteriaNarrative
                ) {
                  return;
                }

                try {
                  setIsCreating(true);

                  const start = Date.now();

                  const response = await fetch(
                    "http://localhost:5045/api/badges",
                    {
                      method: "POST",
                      headers: {
                        "Content-Type": "application/json",
                      },
                      body: JSON.stringify(form),
                    }
                  );

                  if (!response.ok) {
                    throw new Error("Erro ao criar badge");
                  }

                  const elapsed = Date.now() - start;
                  const minTime = 800;

                  if (elapsed < minTime) {
                    await new Promise((resolve) =>
                      setTimeout(resolve, minTime - elapsed)
                    );
                  }

                  const updatedBadge = await getBadges();

                  setBadges(
                    updatedBadge.sort(
                      (a, b) =>
                        new Date(b.createdAt).getTime() -
                        new Date(a.createdAt).getTime()
                    )
                  );

                  setForm({
                    name: "",
                    imageUrl: "",
                    description: "",
                    criteriaNarrative: "",
                  });

                  setIsModalOpen(false);
                  openButtonRef.current?.focus();

                } catch (error) {
                  console.error(error);
                  alert("Erro ao criar badge");
                } finally {
                  setIsCreating(false);
                }
              }}
            >
              <input
                placeholder="Nome do badge"
                className="w-full border p-2 rounded"
                value={form.name}
                onChange={(e) =>
                  setForm({ ...form, name: e.target.value })
                }
              />

              {errors.name && (
                <p className="text-red-600 text-sm">
                  {errors.name}
                </p>
              )}

              <input
                placeholder="URL da imagem"
                className="w-full border p-2 rounded"
                value={form.imageUrl}
                onChange={(e) =>
                  setForm({
                    ...form,
                    imageUrl: e.target.value,
                  })
                }
              />

              {errors.imageUrl && (
                <p className="text-red-600 text-sm">
                  {errors.imageUrl}
                </p>
              )}

              <textarea
                placeholder="Descrição"
                className="w-full border p-2 rounded"
                value={form.description}
                onChange={(e) =>
                  setForm({
                    ...form,
                    description: e.target.value,
                  })
                }
                rows={3}
              />

              {errors.description && (
                <p className="text-red-600 text-sm">
                  {errors.description}
                </p>
              )}

              <textarea
                placeholder="Critérios"
                className="w-full border p-2 rounded"
                value={form.criteriaNarrative}
                onChange={(e) =>
                  setForm({
                    ...form,
                    criteriaNarrative: e.target.value,
                  })
                }
                rows={3}
              />

              {errors.criteriaNarrative && (
                <p className="text-red-600 text-sm">
                  {errors.criteriaNarrative}
                </p>
              )}

              <div className="flex justify-center gap-2 pt-2">
                <button
                  type="button"
                  onClick={() => {
                    setIsModalOpen(false);
                    openButtonRef.current?.focus();
                  }}
                  className="px-4 py-2 bg-gray-200 rounded"
                >
                  Cancelar
                </button>

                <button
                  type="submit"
                  disabled={isCreating}
                  className="px-4 py-2 bg-blue-600 text-white rounded disabled:opacity-50"
                >
                  {isCreating ? "Criando..." : "Criar Badge"}
                </button>
              </div>
            </form>
          </div>
        </div>
      )}
    </div>
  );
};