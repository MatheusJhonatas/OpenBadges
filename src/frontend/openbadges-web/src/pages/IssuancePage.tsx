import { useEffect, useState } from "react";

type Badge = {
  id: string;
  name: string;
};

export const IssuancePage = () => {
  const [badges, setBadges] = useState<Badge[]>([]);

  const [isIssuing, setIsIssuing] = useState(false);

  const [form, setForm] = useState({
    recipientEmail: "",
    recipientName: "",
    badgeClassId: "",
    evidenceUrl: "",
  });

  useEffect(() => {
    loadBadges();
  }, []);

  // Carrega badges disponíveis
  const loadBadges = async () => {
    try {
      const response = await fetch(
        "http://localhost:5045/api/badges"
      );

      const data = await response.json();

      setBadges(data);
    } catch (error) {
      console.error(
        "Erro ao carregar badges",
        error
      );
    }
  };

  // Emissão
  const handleSubmit = async (
    e: React.FormEvent
  ) => {
    e.preventDefault();

    try {
      setIsIssuing(true);

      const response = await fetch(
        "http://localhost:5055/api/Issuances",
        {
          method: "POST",

          headers: {
            "Content-Type": "application/json",
          },

          body: JSON.stringify(form),
        }
      );

      if (!response.ok) {
        throw new Error(
          "Erro ao emitir badge"
        );
      }

      alert("Badge emitido com sucesso!");

      setForm({
        recipientEmail: "",
        recipientName: "",
        badgeClassId: "",
        evidenceUrl: "",
      });

    } catch (error) {
      console.error(error);

      alert("Erro ao emitir badge");

    } finally {

      // 🔥 AQUI ESTÁ O PONTO CORRETO

      setIsIssuing(false);
    }
  };

  return (
    <div className="max-w-2xl mx-auto p-8">
      <h1 className="text-3xl font-bold mb-2">
        Emitir Badge
      </h1>

      <p className="text-gray-600 mb-6">
        Emita credenciais digitais para
        colaboradores
      </p>

      <form
        onSubmit={handleSubmit}
        className="
          bg-white
          border
          rounded-xl
          p-6
          shadow-sm
          space-y-4
        "
      >
        <div>
          <label className="block mb-1 font-medium">
            Nome do Usuário
          </label>

          <input
            type="text"
            className="
              w-full
              border
              rounded
              p-2
            "
            value={form.recipientName}
            onChange={(e) =>
              setForm({
                ...form,
                recipientName:
                  e.target.value,
              })
            }
          />
        </div>

        <div>
          <label className="block mb-1 font-medium">
            Email do Usuário
          </label>

          <input
            type="email"
            className="
              w-full
              border
              rounded
              p-2
            "
            value={form.recipientEmail}
            onChange={(e) =>
              setForm({
                ...form,
                recipientEmail:
                  e.target.value,
              })
            }
          />
        </div>

        <div>
          <label className="block mb-1 font-medium">
            Badge
          </label>

          <select
            className="
              w-full
              border
              rounded
              p-2
            "
            value={form.badgeClassId}
            onChange={(e) =>
              setForm({
                ...form,
                badgeClassId:
                  e.target.value,
              })
            }
          >
            <option value="">
              Selecione um badge
            </option>

            {badges.map((badge) => (
              <option
                key={badge.id}
                value={badge.id}
              >
                {badge.name}
              </option>
            ))}
          </select>
        </div>

        <div>
          <label className="block mb-1 font-medium">
            URL de Evidência
          </label>

          <input
            type="text"
            className="
              w-full
              border
              rounded
              p-2
            "
            value={form.evidenceUrl}
            onChange={(e) =>
              setForm({
                ...form,
                evidenceUrl:
                  e.target.value,
              })
            }
          />
        </div>

        <button
          type="submit"
          disabled={isIssuing}
          className="
            w-full
            bg-blue-600
            text-white
            py-2
            rounded
            disabled:opacity-50
            disabled:cursor-not-allowed
          "
        >
          {isIssuing
            ? "Emitindo badge..."
            : "Emitir Badge"}
        </button>
      </form>
    </div>
  );
};