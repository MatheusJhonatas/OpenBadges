import { X } from "lucide-react"; 

type Props = {
    isOpen: boolean;
    onClose: () => void;
};

export const BadgeModal: React.FC<Props> = ({ isOpen, onClose }) => {
    if (!isOpen) return null;

return (
    <div className="fixed inset-0 bg-black/50 flex items-center justify-center">
      <div className="bg-white p-6 rounded-lg w-96">
        <div className="flex justify-between items-center mb-4">
          <h2 className="text-lg font-bold">Novo Badge</h2>

          <button
            onClick={onClose}
            className="text-gray-500 hover:text-black"
            aria-label="Fechar"
          >
            <X size={20} />
          </button>
        </div>

        <p>Modal funcionando 👊</p>

        <div className="flex justify-center gap-2 pt-4">
          <button
            onClick={onClose}
            className="px-4 py-2 bg-gray-200 rounded"
          >
            Cancelar
          </button>
        </div>
      </div>
    </div>
    );
};

// {/* MODAL */}
//       {isModalOpen && (
//         <div className="fixed inset-0 bg-black/50 flex items-center justify-center">
//           <div
//             ref={modalRef}
//             role="dialog"
//             aria-modal="true"
//             aria-labelledby="modal-title"
//             aria-describedby={undefined}
//             className="bg-white p-6 rounded-lg w-96"
//           >
//             <div className="flex justify-between items-center mb-4">
//               <h2
//                 id="modal-title"
//                 ref={modalTitleRef}
//                 tabIndex={-1}
//                 className="text-lg font-bold"
//               >
//                 Novo Badge
//               </h2>

//               <button
//                 onClick={() => {
//                   resetModal();
//                   setIsModalOpen(false);
//                   openButtonRef.current?.focus();
//                 }}
//                 className="text-gray-500 hover:text-black"
//                 aria-label="Fechar"
//               >
//                 <X size={20} />
//               </button>
//             </div>