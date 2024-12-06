import { WithdrawalStatus } from "@/enums/WithdrawalStatus";

export default function WithdrawalStatusTag({ status }: { status: number }) {
  return (
		<>
			{
        (() => {
          switch (status) {
            case WithdrawalStatus.Pending:
              return <span className="bg-yellow-100 text-yellow-800 text-xs font-medium me-2 px-2.5 py-0.5 rounded border border-yellow-300 capitalize">Chờ xử lý</span>;
            case WithdrawalStatus.Completed:
              return <span className="bg-green-100 text-green-800 text-xs font-medium me-2 px-2.5 py-0.5 rounded border border-green-400 capitalize">Thành công</span>;
						case WithdrawalStatus.Failed:
							return <span className="bg-red-100 text-red-800 text-xs font-medium me-2 px-2.5 py-0.5 rounded border border-red-400 capitalize">Thất bại</span>;
            default:
              return <></>;
          }
        })()
      }
		</>
	);
}