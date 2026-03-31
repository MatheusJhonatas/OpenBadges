type ButtonProps = React.ButtonHTMLAttributes<HTMLButtonElement> & {
  variant?: "primary" | "secondary";
};

export const Button = ({
  children,
  variant = "primary",
  className = "",
  ...props
}: ButtonProps) => {
  const base =
    "inline-flex items-center gap-2 rounded px-6 py-2 text-sm transition";

  const variants = {
    primary: "bg-blue-700 text-white hover:bg-blue-800",
    secondary: "border border-gray-600 hover:bg-gray-200",
  };

  return (
    <button
      type="button"
      className={`${base} ${variants[variant]} ${className}`}
      {...props}
    >
      {children}
    </button>
  );
};