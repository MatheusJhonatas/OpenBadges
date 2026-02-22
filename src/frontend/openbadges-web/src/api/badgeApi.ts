import { API_BASE } from "./client";

export async function getBadges() {
  const response = await fetch(`${API_BASE}/badges`);

  if (!response.ok) {
    throw new Error("Erro ao buscar badges");
  }

  return response.json();
}
export async function createBadge(payload: {
  name: string;
  description: string;
  imageUrl: string;
  criteriaNarrative: string;
}) {
  const response = await fetch(`${API_BASE}/badges`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json"
    },
    body: JSON.stringify(payload)
  });

  if (!response.ok) {
    throw new Error("Erro ao criar badge");
  }

  return response.json();
}