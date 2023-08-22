import { Loja } from "./loja";
import { TipoTransacao } from "./tipoTransacao";

export interface Operacao {
    id?: string,
    tipoTransacaoId?: number,
    lojaId?: string,
    dataOcorrencia?: string,
    valor?: number,
    cpf?: string,
    cartaoTransacao?: string,
    horaOcorrencia?: string,
    tipoTransacao?: TipoTransacao,
    loja?: Loja
}