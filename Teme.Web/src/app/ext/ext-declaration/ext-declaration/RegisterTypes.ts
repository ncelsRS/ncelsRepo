export class RegisterType {
    name: string;
    code: string;
}

export const REGISTER_TYPES: RegisterType[] = [
    { name: 'Регистрация', code: 'Registration' },
    { name: 'Перерегистрация', code: 'Re-registration' },
    { name: 'Внесение изменений', code: 'Edit' }
];