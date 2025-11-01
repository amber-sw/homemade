<#import "template.ftl" as layout>
<@layout.registeredLayout displayMessage=!messagesPerField.existsError('firstName','lastName','email','username','password','password-confirm'); section>
    <#if section = "form">
        <div id="kc-form">
            <div id="kc-form-wrapper">
                <form id="kc-register-form" class="space-y-4" action="${url.registrationAction}" method="post">

                    <#-- First Name Field -->
                    <div>
                        <label for="firstName" class="block text-sm font-medium text-neutral-700 mb-2">
                            ${msg("firstName")}
                        </label>
                        <input
                            type="text"
                            id="firstName"
                            class="w-full px-4 py-2 border ${messagesPerField.existsError('firstName')?then('border-error-300', 'border-neutral-200')} rounded-md focus:outline-none focus:ring-2 focus:ring-primary-500 focus:border-transparent text-neutral-900"
                            name="firstName"
                            value="${(register.formData.firstName!'')}"
                            autocomplete="given-name"
                            aria-invalid="<#if messagesPerField.existsError('firstName')>true</#if>"
                        />
                        <#if messagesPerField.existsError('firstName')>
                            <span class="text-sm text-error-700 mt-1 block" aria-live="polite">
                                ${kcSanitize(messagesPerField.get('firstName'))?no_esc}
                            </span>
                        </#if>
                    </div>

                    <#-- Last Name Field -->
                    <div>
                        <label for="lastName" class="block text-sm font-medium text-neutral-700 mb-2">
                            ${msg("lastName")}
                        </label>
                        <input
                            type="text"
                            id="lastName"
                            class="w-full px-4 py-2 border ${messagesPerField.existsError('lastName')?then('border-error-300', 'border-neutral-200')} rounded-md focus:outline-none focus:ring-2 focus:ring-primary-500 focus:border-transparent text-neutral-900"
                            name="lastName"
                            value="${(register.formData.lastName!'')}"
                            autocomplete="family-name"
                            aria-invalid="<#if messagesPerField.existsError('lastName')>true</#if>"
                        />
                        <#if messagesPerField.existsError('lastName')>
                            <span class="text-sm text-error-700 mt-1 block" aria-live="polite">
                                ${kcSanitize(messagesPerField.get('lastName'))?no_esc}
                            </span>
                        </#if>
                    </div>

                    <#-- Email Field -->
                    <div>
                        <label for="email" class="block text-sm font-medium text-neutral-700 mb-2">
                            ${msg("email")}
                        </label>
                        <input
                            type="email"
                            id="email"
                            class="w-full px-4 py-2 border ${messagesPerField.existsError('email')?then('border-error-300', 'border-neutral-200')} rounded-md focus:outline-none focus:ring-2 focus:ring-primary-500 focus:border-transparent text-neutral-900"
                            name="email"
                            value="${(register.formData.email!'')}"
                            autocomplete="email"
                            aria-invalid="<#if messagesPerField.existsError('email')>true</#if>"
                        />
                        <#if messagesPerField.existsError('email')>
                            <span class="text-sm text-error-700 mt-1 block" aria-live="polite">
                                ${kcSanitize(messagesPerField.get('email'))?no_esc}
                            </span>
                        </#if>
                    </div>

                    <#-- Username Field (if not email as username) -->
                    <#if !realm.registrationEmailAsUsername>
                        <div>
                            <label for="username" class="block text-sm font-medium text-neutral-700 mb-2">
                                ${msg("username")}
                            </label>
                            <input
                                type="text"
                                id="username"
                                class="w-full px-4 py-2 border ${messagesPerField.existsError('username')?then('border-error-300', 'border-neutral-200')} rounded-md focus:outline-none focus:ring-2 focus:ring-primary-500 focus:border-transparent text-neutral-900"
                                name="username"
                                value="${(register.formData.username!'')}"
                                autocomplete="username"
                                aria-invalid="<#if messagesPerField.existsError('username')>true</#if>"
                            />
                            <#if messagesPerField.existsError('username')>
                                <span class="text-sm text-error-700 mt-1 block" aria-live="polite">
                                    ${kcSanitize(messagesPerField.get('username'))?no_esc}
                                </span>
                            </#if>
                        </div>
                    </#if>

                    <#-- Password Field -->
                    <#if passwordRequired??>
                        <div>
                            <label for="password" class="block text-sm font-medium text-neutral-700 mb-2">
                                ${msg("password")}
                            </label>
                            <input
                                type="password"
                                id="password"
                                class="w-full px-4 py-2 border ${messagesPerField.existsError('password')?then('border-error-300', 'border-neutral-200')} rounded-md focus:outline-none focus:ring-2 focus:ring-primary-500 focus:border-transparent text-neutral-900"
                                name="password"
                                autocomplete="new-password"
                                aria-invalid="<#if messagesPerField.existsError('password')>true</#if>"
                            />
                            <#if messagesPerField.existsError('password')>
                                <span class="text-sm text-error-700 mt-1 block" aria-live="polite">
                                    ${kcSanitize(messagesPerField.get('password'))?no_esc}
                                </span>
                            </#if>
                        </div>

                        <#-- Confirm Password Field -->
                        <div>
                            <label for="password-confirm" class="block text-sm font-medium text-neutral-700 mb-2">
                                ${msg("passwordConfirm")}
                            </label>
                            <input
                                type="password"
                                id="password-confirm"
                                class="w-full px-4 py-2 border ${messagesPerField.existsError('password-confirm')?then('border-error-300', 'border-neutral-200')} rounded-md focus:outline-none focus:ring-2 focus:ring-primary-500 focus:border-transparent text-neutral-900"
                                name="password-confirm"
                                autocomplete="new-password"
                                aria-invalid="<#if messagesPerField.existsError('password-confirm')>true</#if>"
                            />
                            <#if messagesPerField.existsError('password-confirm')>
                                <span class="text-sm text-error-700 mt-1 block" aria-live="polite">
                                    ${kcSanitize(messagesPerField.get('password-confirm'))?no_esc}
                                </span>
                            </#if>
                        </div>
                    </#if>

                    <#-- Additional User Attributes -->
                    <#if recaptchaRequired??>
                        <div class="g-recaptcha" data-size="compact" data-sitekey="${recaptchaSiteKey}"></div>
                    </#if>

                    <#-- Register Button -->
                    <div class="pt-2">
                        <button
                            type="submit"
                            class="w-full bg-primary-500 hover:bg-primary-600 text-white font-semibold py-2.5 px-4 rounded-md transition-colors duration-150 focus:outline-none focus:ring-2 focus:ring-primary-500 focus:ring-offset-2"
                        >
                            ${msg("doRegister")}
                        </button>
                    </div>
                </form>
            </div>
        </div>
    <#elseif section = "info">
        <div class="text-center">
            <a href="${url.loginUrl}" class="text-primary-600 hover:text-primary-700 hover:underline">
                ${msg("backToLogin")}
            </a>
        </div>
    </#if>
</@layout.registeredLayout>
