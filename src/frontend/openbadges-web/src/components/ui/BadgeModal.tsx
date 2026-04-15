import { useState, useEffect, useRef } from "react";
import { X } from "lucide-react";

type Props = {
  isOpen: boolean;
  onClose: () => void;
  onSuccess: () => void;
  badge?: {
    id: string;
    name: string;
    description: string;
    imageUrl?: string;
    criteriaNarrative: string;
    version?: number;
  } | null;
};

export const BadgeModal: React.FC<Props> = ({
  isOpen,
  onClose,
  onSuccess,
  badge,
}) => {
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

  const [isCreating, setIsCreating] = useState(false);

  useEffect(() => {
    if (badge) {
      setForm({
        name: badge.name,
        imageUrl: badge.imageUrl || "",
        description: badge.description,
        criteriaNarrative: badge.criteriaNarrative,
      });
    }
  }, [badge]);

  const titleRef = useRef<HTMLHeadingElement>(null);

  useEffect(() => {
    if (isOpen) {
      setTimeout(() => {
        titleRef.current?.focus();
      }, 100);
    }
  }, [isOpen]);

  const reset = () => {
    setForm({
      name: "",
      imageUrl: "",
      description: "",
      criteriaNarrative: "",
    });

    setErrors({
      name: "",
      imageUrl: "",
      description: "",
      criteriaNarrative: "",
    });

    setIsCreating(false);
  };

  if (!isOpen) return null;

  return (
    <div className="fixed inset-0 bg-black/50 flex items-center justify-center">
      <div
        role="dialog"
        aria-modal="true"
        aria-labelledby="modal-title"
        className="bg-white p-6 rounded-lg w-96"
      >
        <div className="flex justify-between items-center mb-4">
          <h2
            id="modal-title"
            ref={titleRef}
            tabIndex={-1}
            className="text-lg font-bold"
          >
            {badge ? "Editar Badge" : "Novo Badge"}
          </h2>

          <button
            onClick={() => {
              reset();
              onClose();
            }}
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
              name: !form.name.trim() ? "Informe o nome do badge" : "",
              imageUrl: !form.imageUrl.trim() ? "Informe a URL da imagem" : "",
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

              await fetch("http://localhost:5045/api/badges", {
                method: "POST",
                headers: {
                  "Content-Type": "application/json",
                },
                body: JSON.stringify(form),
              });

              reset();
              onSuccess();
              onClose();
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
            onChange={(e) => setForm({ ...form, name: e.target.value })}
          />
          {errors.name && (
            <p role="alert" className="text-red-600 text-sm">
              {errors.name}
            </p>
          )}

          <input
            placeholder="URL da imagem"
            className="w-full border p-2 rounded"
            value={form.imageUrl}
            onChange={(e) => setForm({ ...form, imageUrl: e.target.value })}
          />
          {errors.imageUrl && (
            <p role="alert" className="text-red-600 text-sm">
              {errors.imageUrl}
            </p>
          )}

          <textarea
            placeholder="Descrição"
            className="w-full border p-2 rounded"
            value={form.description}
            onChange={(e) => setForm({ ...form, description: e.target.value })}
          />
          {errors.description && (
            <p role="alert" className="text-red-600 text-sm">
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
          />
          {errors.criteriaNarrative && (
            <p role="alert" className="text-red-600 text-sm">
              {errors.criteriaNarrative}
            </p>
          )}

          <div className="flex justify-center gap-2 pt-2">
            <button
              type="button"
              onClick={() => {
                reset();
                onClose();
              }}
              className="px-4 py-2 bg-gray-200 rounded"
            >
              Cancelar
            </button>

            <button
              type="submit"
              disabled={isCreating}
              className="px-4 py-2 bg-blue-600 text-white rounded"
            >
              {isCreating
                ? "Salvando..."
                : badge
                  ? "Salvar alterações"
                  : "Criar Badge"}
            </button>
          </div>
        </form>
      </div>
    </div>
  );
};
