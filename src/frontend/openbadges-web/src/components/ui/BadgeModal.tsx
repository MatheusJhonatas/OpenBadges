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
    templateId?: string;
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
    templateId: "",
    description: "",
    criteriaNarrative: "",
  });

  const [errors, setErrors] = useState({
    name: "",
    templateId: "",
    description: "",
    criteriaNarrative: "",
  });

  const [isCreating, setIsCreating] = useState(false);

  useEffect(() => {
    if (badge) {
      setForm({
        name: badge.name,
        templateId: badge.templateId || "",
        description: badge.description,
        criteriaNarrative: badge.criteriaNarrative,
      });
    } else {
      setForm({
        name: "",
        templateId: "",
        description: "",
        criteriaNarrative: "",
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
      templateId: "",
      description: "",
      criteriaNarrative: "",
    });

    setErrors({
      name: "",
      templateId: "",
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
              templateId: !form.templateId.trim()
                ? "Informe o ID do template"
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
              newErrors.templateId ||
              newErrors.description ||
              newErrors.criteriaNarrative
            ) {
              return;
            }

            try {
              setIsCreating(true);

              const isEdit = !!badge;

              const url = isEdit
                ? `http://localhost:5045/api/badges/${badge.id}`
                : "http://localhost:5045/api/badges";

              const method = isEdit ? "PUT" : "POST";

              await fetch(url, {
                method,
                headers: {
                  "Content-Type": "application/json",
                },
                body: JSON.stringify({
                  ...form,
                  version: badge?.version ?? 0,
                }),
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

          <select
            className="w-full border p-2 rounded"
            value={form.templateId}
            onChange={(e) =>
              setForm({
                ...form,
                templateId: e.target.value,
              })
            }
          >
            <option value="">Selecione um template</option>

            <option value="template-1">Gold</option>

            <option value="template-2">Silver</option>

            <option value="template-3">Bronze</option>

            <option value="template-4">NTT</option>
          </select>
          {errors.templateId && (
            <p role="alert" className="text-red-600 text-sm">
              {errors.templateId}
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
