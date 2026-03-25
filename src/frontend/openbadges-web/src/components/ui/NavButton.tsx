type NavButtonProps = {
  children: React.ReactNode;
  active?: boolean;
  onClick?: () => void;
  icon?: React.ReactNode;
};

export const NavButton = ({ children, active = false, onClick, icon }: NavButtonProps) => {
  const base =
    "px-3 py-1 rounded text-sm font-medium transition-colors focus:outline-none focus:ring-2 focus:ring-blue-600";

  const activeStyle = "bg-blue-600 text-white hover:bg-blue-700";

  const inactiveStyle =
    "text-gray-700 hover:bg-gray-200 hover:text-gray-900";

  return (
    <button
      onClick={onClick}
      aria-pressed={active}
      className={`${base} ${active ? activeStyle : inactiveStyle}`}
    >
      <span className="flex items-center gap-1">
        {icon}
        {children}
      </span>
 
    </button>
  );
};