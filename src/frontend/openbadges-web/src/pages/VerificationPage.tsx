import { useEffect, useState } from "react";

import { useParams } from "react-router-dom";

type Verification = {
  id: string;

  verificationCode: string;

  recipientName: string;

  badgeClassId: string;

  status: string;

  issuedOn: string;

  revokedOn?: string | null;
};

export const VerificationPage = () => {
  const { code } = useParams();

  const [verification, setVerification] =
    useState<Verification | null>(null);

  const [isLoading, setIsLoading] =
    useState(true);

  const [notFound, setNotFound] =
    useState(false);

  useEffect(() => {
    loadVerification();
  }, []);

  const loadVerification = async () => {
    try {
      const response = await fetch(
        `http://localhost:5055/api/Issuances/verify/${code}`
      );

      if (response.status === 404) {
        setNotFound(true);
        return;
      }

      const data = await response.json();

      setVerification(data);

    } catch (error) {

      console.error(
        "Erro ao verificar badge",
        error
      );

    } finally {

      setIsLoading(false);
    }
  };

  if (isLoading) {
    return (
      <div className="p-8">
        Carregando badge...
      </div>
    );
  }

  if (notFound || !verification) {
    return (
      <div className="p-8">
        Badge não encontrada.
      </div>
    );
  }

  return (
    <div className="
      min-h-screen
      bg-gray-100
      flex
      items-center
      justify-center
      p-8
    ">
      <div className="
        bg-white
        rounded-2xl
        shadow-lg
        p-8
        max-w-xl
        w-full
      ">
        <div className="text-center">
          <h1 className="
            text-3xl
            font-bold
            mb-2
          ">
            Badge Verificada
          </h1>

          <p className="
            text-gray-500
            mb-6
          ">
            Credencial digital válida
          </p>
        </div>

        <div className="
          border
          rounded-xl
          p-6
          space-y-4
        ">
          <div>
            <p className="
              text-sm
              text-gray-500
            ">
              Recipient
            </p>

            <p className="font-semibold">
              {verification.recipientName}
            </p>
          </div>

          <div>
            <p className="
              text-sm
              text-gray-500
            ">
              Verification Code
            </p>

            <p className="font-mono">
              {verification.verificationCode}
            </p>
          </div>

          <div>
            <p className="
              text-sm
              text-gray-500
            ">
              Status
            </p>

            <span className="
              inline-flex
              px-3
              py-1
              rounded-full
              text-sm
              font-medium
              bg-green-100
              text-green-700
            ">
              {verification.status}
            </span>
          </div>

          <div>
            <p className="
              text-sm
              text-gray-500
            ">
              Emitido em
            </p>

            <p>
              {new Date(
                verification.issuedOn
              ).toLocaleDateString("pt-BR")}
            </p>
          </div>
        </div>
      </div>
    </div>
  );
};