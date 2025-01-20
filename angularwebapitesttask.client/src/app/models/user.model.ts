export interface CreateUser {
  email: string;
  password: string;
  countryId: number;
  provinceId: number;
}

export interface CreateUserResult {
  email: string;
  error: string;
}
