import { API_BASE } from "./client";

export async function getBadges() {
  const response = await fetch(`${API_BASE}/badges`);

  if (!response.ok) {
    throw new Error("Erro ao buscar badges");
  }

  return response.json();
}