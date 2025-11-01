<#import "template.ftl" as layout>
<@layout.registeredLayout displayMessage=!messagesPerField.existsError('username','password') displayInfo=realm.password && realm.registrationAllowed && !registrationDisabled??; section>
    <#if section = "form">
        <div id="kc-form">
            <div id="kc-form-wrapper">
                <#if realm.password>
                    <form id="kc-form-login" onsubmit="login.disabled = true; return true;" action="${url.loginAction}" method="post">
                        <#-- Username/Email Field -->
                        <div class="mb-4">
                            <label for="username" class="block text-sm font-medium text-neutral-700 mb-2">
                                <#if !realm.loginWithEmailAllowed>${msg("username")}<#elseif !realm.registrationEmailAsUsername>${msg("usernameOrEmail")}<#else>${msg("email")}</#if>
                            </label>
                            <input
                                tabindex="1"
                                id="username"
                                class="w-full px-4 py-2 border ${messagesPerField.existsError('username','password')?then('border-error-300', 'border-neutral-200')} rounded-md focus:outline-none focus:ring-2 focus:ring-primary-500 focus:border-transparent text-neutral-900"
                                name="username"
                                value="${(login.username!'')}"
                                type="text"
                                autofocus
                                autocomplete="username"
                                aria-invalid="<#if messagesPerField.existsError('username','password')>true</#if>"
                            />
                            <#if messagesPerField.existsError('username','password')>
                                <span class="text-sm text-error-700 mt-1 block" aria-live="polite">
                                    ${kcSanitize(messagesPerField.getFirstError('username','password'))?no_esc}
                                </span>
                            </#if>
                        </div>

                        <#-- Password Field -->
                        <div class="mb-4">
                            <label for="password" class="block text-sm font-medium text-neutral-700 mb-2">
                                ${msg("password")}
                            </label>
                            <input
                                tabindex="2"
                                id="password"
                                class="w-full px-4 py-2 border ${messagesPerField.existsError('username','password')?then('border-error-300', 'border-neutral-200')} rounded-md focus:outline-none focus:ring-2 focus:ring-primary-500 focus:border-transparent text-neutral-900"
                                name="password"
                                type="password"
                                autocomplete="current-password"
                                aria-invalid="<#if messagesPerField.existsError('username','password')>true</#if>"
                            />
                        </div>

                        <#-- Remember Me and Forgot Password -->
                        <div class="flex items-center justify-between mb-6">
                            <#if realm.rememberMe && !usernameHidden??>
                                <div class="flex items-center">
                                    <input
                                        tabindex="3"
                                        id="rememberMe"
                                        name="rememberMe"
                                        type="checkbox"
                                        class="w-4 h-4 text-primary-600 border-neutral-300 rounded focus:ring-primary-500"
                                        <#if login.rememberMe??>checked</#if>
                                    />
                                    <label for="rememberMe" class="ml-2 text-sm text-neutral-700">
                                        ${msg("rememberMe")}
                                    </label>
                                </div>
                            <#else>
                                <div></div>
                            </#if>

                            <#if realm.resetPasswordAllowed>
                                <a tabindex="5" href="${url.loginResetCredentialsUrl}" class="text-sm text-primary-600 hover:text-primary-700 hover:underline">
                                    ${msg("doForgotPassword")}
                                </a>
                            </#if>
                        </div>

                        <#-- Login Button -->
                        <div class="mb-4">
                            <input
                                type="hidden"
                                id="id-hidden-input"
                                name="credentialId"
                                <#if auth.selectedCredential?has_content>value="${auth.selectedCredential}"</#if>
                            />
                            <button
                                tabindex="4"
                                class="w-full bg-primary-500 hover:bg-primary-600 text-white font-semibold py-2.5 px-4 rounded-md transition-colors duration-150 focus:outline-none focus:ring-2 focus:ring-primary-500 focus:ring-offset-2"
                                name="login"
                                id="kc-login"
                                type="submit"
                            >
                                ${msg("doLogIn")}
                            </button>
                        </div>
                    </form>
                </#if>
            </div>
        </div>
    <#elseif section = "info" >
        <#if realm.password && realm.registrationAllowed && !registrationDisabled??>
            <div class="text-center">
                <span class="text-neutral-700">${msg("noAccount")}</span>
                <a tabindex="6" href="${url.registrationUrl}" class="text-primary-600 hover:text-primary-700 hover:underline font-medium ml-1">
                    ${msg("doRegister")}
                </a>
            </div>
        </#if>
    <#elseif section = "socialProviders" >
        <#if realm.password && social.providers??>
            <div id="kc-social-providers" class="mt-4">
                <hr class="my-4"/>
                <h4 class="text-sm text-neutral-600 mb-3">${msg("identity-provider-login-label")}</h4>
                <ul class="space-y-2">
                    <#list social.providers as p>
                        <li>
                            <a
                                id="social-${p.alias}"
                                class="block w-full px-4 py-2 text-center border border-neutral-300 rounded-md hover:bg-neutral-50 text-neutral-700 transition-colors duration-150"
                                type="button"
                                href="${p.loginUrl}"
                            >
                                <#if p.iconClasses?has_content>
                                    <i class="${properties.kcCommonLogoIdP!} ${p.iconClasses!}" aria-hidden="true"></i>
                                    <span>${p.displayName!}</span>
                                <#else>
                                    <span>${p.displayName!}</span>
                                </#if>
                            </a>
                        </li>
                    </#list>
                </ul>
            </div>
        </#if>
    </#if>
</@layout.registeredLayout>
