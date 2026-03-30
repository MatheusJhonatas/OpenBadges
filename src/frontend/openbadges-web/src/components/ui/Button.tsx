
type ButtonProps = {
  children: React.ReactNode;
  variant?: "primary" | "secondary";
  onClick?: () => void;
};

export const Button = ({ children, variant = "primary", onClick }: ButtonProps) => {
 const base = "inline-flex items-center gap-2 rounded px-6 py-2 text-sm transition";

  const variants = {
    primary: "bg-blue-600 text-white hover:bg-blue-800",
    secondary: "border border-gray-600 hover:bg-gray-200",
  };

  return (
    <button className={`${base} ${variants[variant]}`} onClick={onClick}>
      {children}
    </button>
  );
};