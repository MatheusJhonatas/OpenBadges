import { useEffect, useState } from "react";

type Issuance = {
  id: string;

  recipientName: string;

  recipientEmail: string;

  badgeClassId: string;

  status: string;

  issuedOn: string;

  revokedOn?: string | null;
};

export const AdminIssuancesPage = () => {
  const [issuances, setIssuances] =
    useState<Issuance[]>([]);

  const [isLoading, setIsLoading] =
    useState(true);

  useEffect(() => {
    loadIssuances();
  }, []);

  const loadIssuances = async () => {
    try {
      const response = await fetch(
        "http://localhost:5055/api/Issuances"
      );

      const data = await response.json();

      setIssuances(data);

    } catch (error) {

      console.error(
        "Erro ao carregar emissões",
        error
      );

    } finally {

      setIsLoading(false);
    }
  };

  return (
    <div className="max-w-7xl mx-auto p-8">
      <h1 className="text-3xl font-bold mb-2">
        Emissões
      </h1>

      <p className="text-gray-600 mb-6">
        Gerencie badges emitidos para
        colaboradores
      </p>

      <div className="
        bg-white
        border
        rounded-xl
        shadow-sm
        overflow-hidden
      ">
        {isLoading ? (

          <div className="p-6">
            Carregando emissões...
          </div>

        ) : issuances.length === 0 ? (

          <div className="p-6">
            Nenhuma emissão encontrada.
          </div>

        ) : (

          <table className="w-full">
            <thead className="bg-gray-50">
              <tr className="text-left">
                <th className="p-4">
                  Usuário
                </th>

                <th className="p-4">
                  Email
                </th>

                <th className="p-4">
                  Status
                </th>

                <th className="p-4">
                  Emitido em
                </th>
              </tr>
            </thead>

            <tbody>
              {issuances.map(
                (issuance) => (
                  <tr
                    key={issuance.id}
                    className="
                      border-t
                      hover:bg-gray-50
                    "
                  >
                    <td className="p-4">
                      {issuance.recipientName}
                    </td>

                    <td className="p-4">
                      {issuance.recipientEmail}
                    </td>

                    <td className="p-4">
                      <span
                        className="
                          px-2
                          py-1
                          rounded-full
                          text-xs
                          font-medium
                          bg-green-100
                          text-green-700
                        "
                      >
                        {issuance.status}
                      </span>
                    </td>

                    <td className="p-4">
                      {new Date(
                        issuance.issuedOn
                      ).toLocaleDateString(
                        "pt-BR"
                      )}
                    </td>
                  </tr>
                )
              )}
            </tbody>
          </table>
        )}
      </div>
    </div>
  );
};