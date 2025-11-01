<#import "template.ftl" as layout>
<@layout.registeredLayout displayMessage=false; section>
    <#if section = "form">
        <div id="kc-error-message">
            <div class="mb-6 p-4 bg-error-50 border border-error-300 rounded-md">
                <div class="flex">
                    <div class="flex-shrink-0">
                        <svg class="h-5 w-5 text-error-600" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 20 20" fill="currentColor">
                            <path fill-rule="evenodd" d="M10 18a8 8 0 100-16 8 8 0 000 16zM8.707 7.293a1 1 0 00-1.414 1.414L8.586 10l-1.293 1.293a1 1 0 101.414 1.414L10 11.414l1.293 1.293a1 1 0 001.414-1.414L11.414 10l1.293-1.293a1 1 0 00-1.414-1.414L10 8.586 8.707 7.293z" clip-rule="evenodd" />
                        </svg>
                    </div>
                    <div class="ml-3">
                        <h3 class="text-sm font-semibold text-error-900">
                            ${kcSanitize(msg("errorTitle"))?no_esc}
                        </h3>
                        <div class="mt-2 text-sm text-error-800">
                            <p>${kcSanitize(message.summary)?no_esc}</p>
                        </div>
                    </div>
                </div>
            </div>

            <#if client?? && client.baseUrl?has_content>
                <p class="text-center">
                    <a
                        href="${client.baseUrl}"
                        class="inline-block bg-primary-500 hover:bg-primary-600 text-white font-semibold py-2.5 px-6 rounded-md transition-colors duration-150 focus:outline-none focus:ring-2 focus:ring-primary-500 focus:ring-offset-2"
                    >
                        ${kcSanitize(msg("backToApplication"))?no_esc}
                    </a>
                </p>
            <#else>
                <p class="text-center">
                    <a
                        href="${url.loginUrl}"
                        class="text-primary-600 hover:text-primary-700 hover:underline font-medium"
                    >
                        ${kcSanitize(msg("backToLogin"))?no_esc}
                    </a>
                </p>
            </#if>
        </div>
    </#if>
</@layout.registeredLayout>
